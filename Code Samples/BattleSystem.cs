//Below is a Code Sample for the Battle System at the End of the Game

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, SESSION, PLAYERATTACK, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    Unit playerUnit;
    Unit enemyUnit;

    Animator playerAnimator;
    Animator enemyAnimator;

    Button healButton;

    //Time being able to use the Button
    public int healButtonUse;
    public int healButtonAmount;

    public static BattleState state;

    [Header("UI Objects")]
    public GameObject BossDisplay;
    public GameObject playerPrefab;
    public GameObject playerHealButton;
    public GameObject enemyPrefab;
    public Slider hpSlider;

    [Header("Wordle Objects")]
    public GameObject wordlePlayBoard;
    public GameObject wordleBoardLine;
    public GameObject wordleInstructWordText;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        SetupBattle();
    }

    void Update() {
        //The update functioin is to ensure which state we are in.
        StartCoroutine(AttackingSequence());
        StartCoroutine(EnemyTurn());
    }

    void SetupBattle(){
        GameObject playerGO = Instantiate(playerPrefab);
        playerUnit = playerGO.GetComponentInChildren<Unit>();
        playerAnimator = playerGO.GetComponentInChildren<Animator>();

        GameObject enemyGO = Instantiate(enemyPrefab);
        enemyUnit = enemyGO.GetComponentInChildren<Unit>();
        enemyAnimator = enemyGO.GetComponentInChildren<Animator>();

        healButton = playerHealButton.GetComponent<Button>();

        SetHUD(playerUnit);

        state = BattleState.PLAYERTURN;
    }

    IEnumerator PlayerAttack(){
        // Set up Wordle Session
        wordlePlayBoard.SetActive(true);
        wordleInstructWordText.SetActive(true);
        wordleBoardLine.SetActive(true);

        //Disable BossDisplay
        BossDisplay.SetActive(false);

        state = BattleState.SESSION;
        yield return new WaitForSeconds(2f);
    }

    IEnumerator PlayerHeal(){
        healButtonAmount -= 1;

        playerUnit.Heal();

        SetHP(playerUnit.currentHP);
        state = BattleState.SESSION;

        if (healButtonAmount <= healButtonUse){
            healButton.interactable = false;
            healButton.FindSelectableOnLeft().Select();
        }

        yield return new WaitForSeconds(1f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    //Start the Animation and Attacking Sequence
    IEnumerator AttackingSequence(){
        if (state == BattleState.PLAYERATTACK){
            BossDisplay.SetActive(true);
            playerAnimator.SetBool("isAttacking", true);

            state = BattleState.SESSION;

            yield return new WaitForSeconds(1f);

            playerAnimator.SetBool("isAttacking", false);

            StartCoroutine(enemyUnit.EnemyTakeDamage(playerUnit.damage, enemyAnimator, returnValue => {
                if (returnValue){
                    //End Battle
                    state = BattleState.WON;
                    EndBattle();
                } else {
                    //Enemy's Turn
                    state = BattleState.ENEMYTURN;
                    StartCoroutine(EnemyTurn());
                }
            }));
        }
    }

    //Start the Animation and Attacking Sequence
    IEnumerator EnemyTurn(){
        if (state == BattleState.ENEMYTURN){
            BossDisplay.SetActive(true);

            enemyAnimator.SetBool("isAttacking", true);

            state = BattleState.SESSION;

            yield return new WaitForSeconds(1f);

            enemyAnimator.SetBool("isAttacking", false);

            StartCoroutine(playerUnit.PlayerTakeDamage(enemyUnit.damage, playerAnimator, returnValue => {

                SetHP(playerUnit.currentHP);

                if (returnValue){
                    state = BattleState.LOST;
                    EndBattle();
                } else {
                    state = BattleState.PLAYERTURN;
                    BossDisplay.SetActive(true);
                    PlayerAttack();
                }
            }));
        }
    }

    void EndBattle(){
        if (state == BattleState.WON){
            //Player Won
            enemyAnimator.SetTrigger("isDead");

            //Add score
            ActiveGameState.PlayerScore += 10;
        } else  if (state == BattleState.LOST){
            //Enemy Won
            playerAnimator.SetTrigger("isDead");
        }
    }

    public void OnAttackButton(){
        if(state != BattleState.PLAYERTURN || state == BattleState.SESSION){
            return;
        }
        
        StartCoroutine(PlayerAttack());
    }

    public void OnHealButton(){
        if(state != BattleState.PLAYERTURN || state == BattleState.SESSION){
            return;
        }
        
        StartCoroutine(PlayerHeal());
    }

    void SetHUD(Unit unit){
        hpSlider.maxValue = unit.maxHP;
        hpSlider.value = unit.currentHP;
    }

    void SetHP(int hp){
        hpSlider.value = hp;
    }
}

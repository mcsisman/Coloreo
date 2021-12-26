using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static Table;
using static MovesText;
using static BallCountText;
using static WinCondText;

public class TouchHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI movesText;
    [SerializeField] private GameObject successModal;
    [SerializeField] private GameObject failModal;
    [SerializeField] private GameObject unlockLevelModal;
    [SerializeField] private GameObject lastLevelModal;
    [SerializeField] private float colorChangeAnimationDuration = 0.25f;
    [SerializeField] private int oneInChanceOfBonusSquare = 40;
    [SerializeField] private int temporarySizeDuration = 3;
    private int temporarySizeCounter = 0;
    private Collider currentball;
    private int currentSelectedRow;
    private int currentSelectedColumn;
    public int selectionWidth = 3;
    private Color selectionColor;
    private MovesText mt;
    public int[] colorCount;
    public bool touchDisabled = false;
    public WinCondText winCondText;
    public TextMeshProUGUI winCondUIText;
    private BallCountText ballCountText;
    public TextMeshProUGUI text;
    private bool failModalHidden = false;
    

    Table t;
    // Start is called before the first frame update
    void Start(){
        winCondText = GameObject.Find("WinCondText").GetComponent<WinCondText>();
        mt = GameObject.Find("MovesText").GetComponent<MovesText>();
        ballCountText = GameObject.Find("BallCountText").GetComponent<BallCountText>();
        
        selectionColor = new Color(166 / 255f, 148/255f, 148/255f, .4f);
        t = new Table();

        Button btn = GetComponent<Button>();
        
        if (GameObject.Find("NextLevelButton-DEBUG")){
            btn = GameObject.Find("NextLevelButton-DEBUG").GetComponent<Button>();
            btn.onClick.AddListener(LevelSucceed);
        }
        
    }

    // Update is called once per frame
    void Update()
    {   
        TouchListenerForBalls();
    }
    void TouchListenerForBalls() {
        if (touchDisabled) {
            return;
        }
        foreach (Touch touch in Input.touches) {
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            RaycastHit hit; 

            if (touch.phase == TouchPhase.Began) {
                if (Physics.Raycast(ray, out hit)) {
                    currentSelectedRow = hit.collider.GetComponent<BallInformation>().row;
                    currentSelectedColumn = hit.collider.GetComponent<BallInformation>().column;
                    currentball = hit.collider;
                    AddSelectionBackground();
                }
            }
            if (touch.phase == TouchPhase.Moved) {
                if (Physics.Raycast(ray, out hit)) {
                    RemoveSelectionBackground();
                    currentSelectedRow = hit.collider.GetComponent<BallInformation>().row;
                    currentSelectedColumn = hit.collider.GetComponent<BallInformation>().column;
                    currentball = hit.collider;
                    AddSelectionBackground();
                } else {
                    RemoveSelectionBackground();
                }
            }

            if (touch.phase == TouchPhase.Ended) {
                if (Physics.Raycast(ray, out hit)) {
                    currentball = hit.collider;
                    RemoveSelectionBackground();

                    // Get row-column data of selected ball
                    GameObject lm = GameObject.Find("LevelManager");
                    
                    lm.GetComponent<LevelManager>().moves = lm.GetComponent<LevelManager>().moves - 1;
                    mt.UpdateMovesText(movesText);
                    ChangeBallColors(selectionWidth, currentSelectedRow, currentSelectedColumn);
                    ParallelCoroutine();
                    Invoke("ControlSelectionSize", colorChangeAnimationDuration);

                } else {
                    RemoveSelectionBackground();
                }
            }
        }
    }
    public bool CheckWinConditions(GameObject lm) {
        string[] winConds = lm.GetComponent<LevelManager>().lvl.winConds;
        string[] colors = new string[winConds.Length];
        int result = 0;
        for(int i = 0; i < winConds.Length; i++) {
            if (CheckCondition(winConds[i], lm)) {
                colors[i] = "#4BDD85";
            } else {
                result++;
                colors[i] = "#EA7272";
            }
        }
        
        winCondText.SetWinConditionText(winCondUIText, colors);
        return result == 0;
    }
    public bool CheckCondition(string winCond, GameObject lm) {
        int[] colorCountArr = lm.GetComponent<LevelManager>().colorCount;
        string[] cond = winCond.Split(' ');
        string[] operators = new string[(cond.Length - 1)/2];
        string[] operands = new string[(cond.Length + 1)/2];
        
        // There are (operators + 1) number of final operands
        List<int> finalOperands = new List<int>();
        List<string> conditionOperators = new List<string>();

        // Fill out the operands and operators arrays
        for (int i = 0; i < cond.Length; i++){
            if (i % 2 == 0){
                operands[i / 2] = cond[i];
            }
            else{
                operators[(i - 1) / 2] = cond[i];
            }
        }
        
        // Initialize leftmost hand side to the first value in the win condition
        finalOperands.Add(GetColorCount(operands[0], colorCountArr));

        int finalOperandIndex = 0;
        int conditionOperatorIndex = -1;
        
        //x + y = 2
        while (true){
            int operatorIndex;
            //Calculate the final operands
            for (operatorIndex = conditionOperatorIndex + 1; operatorIndex < operators.Length; operatorIndex++){
                if (operators[operatorIndex] == "+"){
                    finalOperands[finalOperandIndex] += GetColorCount(operands[operatorIndex + 1], colorCountArr);
                }
                else if (operators[operatorIndex] == "-"){
                    finalOperands[finalOperandIndex] -= GetColorCount(operands[operatorIndex + 1], colorCountArr);
                }
                // if the operator is the condition operator, break the loop, initialize right hand side and swith to it
                else{
                    // Add into condition operators list and set the index
                    conditionOperators.Add(operators[operatorIndex]);
                    conditionOperatorIndex = operatorIndex;
                    
                    // Go to the next finalOperand
                    finalOperandIndex++;
                    
                    //Initialize next operand
                    finalOperands.Add(GetColorCount(operands[operatorIndex + 1], colorCountArr));
                    break;
                }
            }
            
            // if reached the last operator in the win condition
            if (operatorIndex == operators.Length){
                break;
            }
        }
        bool winCondSatisfied = true;
        for (int i = 0; i < conditionOperators.Count; i++){
            // if the previous condition is satisfied, check the next one, if not; return false
            if (winCondSatisfied){
                if (conditionOperators[i] == "<") {
                    winCondSatisfied = finalOperands[i] < finalOperands[i + 1];
                }
                if (conditionOperators[i] == ">") {
                    winCondSatisfied = finalOperands[i] > finalOperands[i + 1];
                }
                if (conditionOperators[i] == "=") {
                    winCondSatisfied = finalOperands[i] == finalOperands[i + 1];
                } 
            }
        }
        return winCondSatisfied;
    }
    public int GetColorCount(string colorCode, int[] colorCountArr) {
        
        if ( colorCode == "xx") {
            return colorCountArr[0];
        }
        else if (colorCode == "yy") {
            return colorCountArr[1];
        }
        else if (colorCode == "zz") {
            return colorCountArr[2];
        }
        else if (colorCode == "tt") {
            return colorCountArr[3];
        } else {
            return int.Parse(colorCode);
        }
    }
    void RemoveSelectionBackground() {
        for (int i = currentSelectedRow - (selectionWidth - 1) / 2; i <= currentSelectedRow + (selectionWidth - 1) / 2; i++) {
            for (int j = currentSelectedColumn - (selectionWidth - 1) / 2; j <= currentSelectedColumn + (selectionWidth - 1) / 2; j++) {
                GameObject tmp;
                tmp = GameObject.Find("cell" + i + "," + j + "(Clone)");

                // If the selected ball is not a black circle
                if (tmp != null) {
                     tmp.GetComponent<SpriteRenderer>().color = new Color(0.9058824f, 0.9058824f, 0.945098f, 0);
                }
            }
        }
    }

    void AddSelectionBackground() {
        for (int i = currentSelectedRow - (selectionWidth - 1) / 2; i <= currentSelectedRow + (selectionWidth - 1) / 2; i++) {
            for (int j = currentSelectedColumn - (selectionWidth - 1) / 2; j <= currentSelectedColumn + (selectionWidth - 1) / 2; j++) {
                GameObject tmp;
                
                tmp = GameObject.Find("cell" + i + "," + j + "(Clone)");
                if (tmp != null ) {
                    // Black circle
                    if (tmp.GetComponent<BallInformation>().colorEnum == 5){
                        tmp.GetComponent<SpriteRenderer>().color = new Color(.1f, .1f, .1f, 1f);
                        
                    }
                    // Skull
                    else if( tmp.GetComponent<BallInformation>().colorEnum == 4 ) {
                        tmp.GetComponent<SpriteRenderer>().color = new Color(.8f, 0, 0, 1f);
                    }
                    // The rest
                    else {
                        tmp.GetComponent<SpriteRenderer>().color = selectionColor;
                    }
                    
                }
            }
        }
    }

    void ChangeBallColors(int selectionWidth, int row, int column) {
        failModalHidden = true;
        // If selection width is 3, change 1 left, itself, 1 right
        for (int i = row - (selectionWidth - 1) / 2; i <= row + (selectionWidth - 1) / 2; i++) {
            for (int j = column - (selectionWidth - 1) / 2; j <= column + (selectionWidth - 1) / 2; j++) {
                GameObject tmp;
                tmp = GameObject.Find("cell" + i + "," + j + "(Clone)");
                if (tmp != null){
                    int colorNo = tmp.GetComponent<BallInformation>().colorEnum;
                    // If skull is selected
                    if( colorNo == 4 ){
                        if ( failModalHidden ){
                            LevelFailed();
                            GameObject.Find("FailedText").GetComponent<TextMeshProUGUI>().SetText("Failure!   <sprite index= 4>");
                            failModalHidden = false;
                            break;
                        }
                        
                    }
                    // If the selected color is a changeable color
                    if ( colorNo != 5 & colorNo != 4 ){
                        GameObject child;
                        child = tmp.transform.GetChild(0).gameObject;
                        AnimateColorChange(child, tmp);
                    }

                    if (colorNo == 7 || colorNo == 8 || colorNo == 9){
                        BonusSquareClicked(colorNo);
                    }
                }
            }
        }
        
    }

    void AnimateColorChange(GameObject obj, GameObject parent) {
        IEnumerator coroutine;
        coroutine = ColorAnimation(obj, parent);
        StartCoroutine(coroutine);
    }
    void ParallelCoroutine() {
        IEnumerator coroutine;
        coroutine = CheckConds();
        StartCoroutine(coroutine);
    }
    IEnumerator ColorAnimation(GameObject obj, GameObject parent) {
        touchDisabled = true;
        float elapsedTime = 0;
        float waitTime = colorChangeAnimationDuration;

        int previousColor = parent.GetComponent<BallInformation>().colorEnum;
        int newEnum = 0;
        int newColor = 0;
        Color oldColor = obj.GetComponent<SpriteRenderer>().color;
        LevelManager lvManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        int[] allowedColors = lvManager.lvl.allowedColors;
        int changeableColor = lvManager.lvl.changeableColor;

        while (elapsedTime < waitTime) {
            newEnum = Random.Range(0, changeableColor);
            newColor = allowedColors[newEnum] - 1;
            parent.GetComponent<BallInformation>().colorEnum = newColor;
            obj.GetComponent<SpriteRenderer>().color = Color.Lerp(oldColor, t.GetColorOfIndex(newColor + 1), (elapsedTime / waitTime));
            
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        LevelManager lm = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        // If bonus square occurs! and if it's not last move
        if (Random.Range(0, oneInChanceOfBonusSquare) == 0 && lm.moves > 1){
            InsertBonusSquare(obj, parent);
            // this ensures if a bonus square spawns, count is right
            newColor = -1;
        }
        else{
            obj.GetComponent<SpriteRenderer>().sprite = obj.GetComponent<BallInformation>().circleSprite;
            obj.GetComponent<SpriteRenderer>().color = t.GetColorOfIndex(newColor + 1);
        }
        
        // Update color counts
        for( int i = 1; i <= changeableColor; i++ ) {
            int colorIndex = allowedColors[i - 1] - 1;
            
            if( previousColor == colorIndex ) {
                lm.colorCount[colorIndex] = lm.colorCount[colorIndex] - 1;
            }
            if ( newColor == colorIndex ) {
                lm.colorCount[colorIndex] = lm.colorCount[colorIndex] + 1;
            }
        }
        ballCountText.UpdateColorCountText(text);
        touchDisabled = false;
        yield return null;
    }

    IEnumerator CheckConds() {
        float elapsedTime = 0;
        float waitTime = colorChangeAnimationDuration;
        while (elapsedTime < waitTime) {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        GameObject lm = GameObject.Find("LevelManager");
        
        if (CheckWinConditions(lm)) {
            // If skull is clicked
            if (failModalHidden){
                LevelSucceed();
            }
            
        } else if (lm.GetComponent<LevelManager>().moves < 1) {
            if( failModalHidden ) {
                LevelFailed();
            }
            
        }

        yield return null;
    }
    void LevelSucceed(){
        selectionWidth = 3;
        SetCircleHideAnimationParameter();
        Destroy(GameObject.Find("Table(Clone)"), .75f);
        
        // If reached max level
        LevelManager lvlMan =  GameObject.Find("LevelManager").GetComponent<LevelManager>();
        if (lvlMan.maxLv == lvlMan.lvl.levelNo){
            Object.Instantiate(lastLevelModal, GameObject.Find("Canvas").GetComponent<Transform>());
        }
        else{
            Object.Instantiate(successModal, GameObject.Find("Canvas").GetComponent<Transform>());
            SetSuccessModalInfo();
            Button btn = GetComponent<Button>();
            btn = GameObject.Find("StartButton").GetComponent<Button>();
            btn.onClick.AddListener(NextLevelBtnOnClick);
        }
        
    }
    public void LevelFailed() {
        selectionWidth = 3;
        SetCircleHideAnimationParameter();
        Destroy(GameObject.Find("Table(Clone)"), .75f);
        Object.Instantiate(failModal, GameObject.Find("Canvas").GetComponent<Transform>());

        SetFailModalInfo();
        Button btn = GetComponent<Button>();
        btn = GameObject.Find("RetryButton").GetComponent<Button>();
        btn.onClick.AddListener(RetryLevel);

    }

    void NextLevelBtnOnClick() {
        int lastUnlockedLv = PlayerPrefs.GetInt("lastUnlockedLv");
        int currentLv = GameObject.Find("LevelManager").GetComponent<LevelManager>().lvl.levelNo;

        // If the player is at the last unlocked lvl, show the unlock modal
        if( lastUnlockedLv <= currentLv ) {
            Destroy(GameObject.Find("LevelEndModalSuccess(Clone)"), 0);
            CreateLevelUnlockModal();
        }
        else {
            LoadNextLevel();
        }
    }

    void CreateLevelUnlockModal() {
        Object.Instantiate(unlockLevelModal, GameObject.Find("Canvas").transform);
    }

    public void DestroySuccessModal() {
        // Remove backdrop from the modal
        GameObject modal;
        if( modal = GameObject.Find("LevelEndModalSuccess(Clone)") ) {
            Animator animator = GameObject.Find("SuccessModal").GetComponent<Animator>();
            animator.SetBool("isHidden", true);

            GameObject.Find("ExitButton").GetComponent<Button>().interactable = false;
            GameObject.Find("StartButton").GetComponent<Button>().interactable = false;
            Image img = modal.GetComponent<Image>();
            var tempColor = img.color;
            tempColor.a = 0f;
            img.color = tempColor;

            // .75 for the modal removal animation
            Destroy(modal, .75f);
        }
    }
    public void LoadNextLevel(){
        selectionWidth = 3;
        DestroySuccessModal();
        // .3 for the wait time to create table, until modal slides away
        Invoke("LoadNextLevelDelayed", .3f);
    }
    
    public void RetryLevel() {
        selectionWidth = 3;

        if (GameObject.Find("FailModal")){
            Animator animator = GameObject.Find("FailModal").GetComponent<Animator>();
            animator.SetBool("isHidden", true);
            GameObject.Find("ExitButton").GetComponent<Button>().interactable = false;
            GameObject.Find("RetryButton").GetComponent<Button>().interactable = false;
            
            // Remove backdrop from the modal
            GameObject modal = GameObject.Find("LevelEndModalFail(Clone)");
            Image img = modal.GetComponent<Image>();
            var tempColor = img.color;
            tempColor.a = 0f;
            img.color = tempColor;

            // .75 for the modal removal animation
            Destroy(modal, .75f);
        }
        
        // .3 for the wait time to create table, until modal slides away
        Invoke("LoadLevelDelayed", .3f);
    }
    void LoadNextLevelDelayed() {
        GameObject lm = GameObject.Find("LevelManager");
        lm.GetComponent<LevelManager>().LoadNextLevel();
    }
    public void LoadLevelDelayed() {
        GameObject lm = GameObject.Find("LevelManager");
        lm.GetComponent<LevelManager>().LoadLevel(lm.GetComponent<LevelManager>().currentLevel);
    }
    void SetSuccessModalInfo() {
        GameObject lm = GameObject.Find("LevelManager");
        int currentLv = lm.GetComponent<LevelManager>().currentLevel + 1;
        GameObject.Find("ModalLevelText").GetComponent<TextMeshProUGUI>().SetText("Level " + currentLv);
        SetModalGoalText(true);
    }
    void SetFailModalInfo()
    {
        GameObject lm = GameObject.Find("LevelManager");
        int currentLv = lm.GetComponent<LevelManager>().currentLevel;
        GameObject.Find("ModalLevelText").GetComponent<TextMeshProUGUI>().SetText("Level " + currentLv);
        SetModalGoalText(false);
    }
    void SetModalGoalText(bool isNext) {
        string[] winConds;
        
        if (isNext){
            winConds = GameObject.Find("LevelManager").GetComponent<LevelManager>().lvl.nextWinCons;
        }
        else{
            winConds = GameObject.Find("LevelManager").GetComponent<LevelManager>().lvl.winConds;
        }
        
        string display = "";
        for (int i = 0; i < winConds.Length; i++){
            if(i != winConds.Length - 1 ) {
                display = display + winCondText.ConvertConditionToDisplay(winConds[i]) + "   --   ";
            }
            else {
                display = display + winCondText.ConvertConditionToDisplay(winConds[i]);
            }
            
            display = "<color=" + "#EA7272" + ">" + display + "</color>";
        }
        GameObject.Find("GoalTextBottom").GetComponent<TextMeshProUGUI>().SetText(display);
    }

    void SetCircleHideAnimationParameter()
    {
        GameObject table = GameObject.Find("Table(Clone)");
        GameObject cell;
        for(int i = 0; i < 64; i++)
        {
            cell = table.transform.GetChild(i).gameObject;
            cell.GetComponent<BoxCollider>().enabled = false;
            cell.transform.GetChild(0).gameObject.GetComponent<Animator>().SetBool("isCircleHidden", true);
        }
    }

    public void EnableTouchWithDelay(){
        Invoke("EnableTouch", .3f);
    }

    public void EnableTouch(){
        touchDisabled = false;
    }

    void InsertBonusSquare(GameObject obj, GameObject parent){
        int random = Random.Range(0, 3);
        if (random == 0){
            obj.GetComponent<SpriteRenderer>().sprite = obj.GetComponent<BallInformation>().growSprite;
            parent.GetComponent<BallInformation>().colorEnum = 7;
        }
        else if (random == 1){
            obj.GetComponent<SpriteRenderer>().sprite = obj.GetComponent<BallInformation>().shrinkSprite;
            parent.GetComponent<BallInformation>().colorEnum = 8;
        }
        else if (random == 2){
            obj.GetComponent<SpriteRenderer>().sprite = obj.GetComponent<BallInformation>().plusSprite;
            parent.GetComponent<BallInformation>().colorEnum = 9;
        }
        //green obj.GetComponent<SpriteRenderer>().color = new Color(0.1254902f, 0.7490196f, 0.4196078f, 1f);
        obj.GetComponent<SpriteRenderer>().color = new Color(0.2f, 0.227451f, 0.3960784f, 1f);
        //obj.GetComponent<SpriteRenderer>().color = new Color(0.9686275f, 0.7176471f, 0.1921569f, 1f);
        //obj.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1f);
        
    }

    void ControlSelectionSize(){
        if (temporarySizeCounter == temporarySizeDuration){
            selectionWidth = 3;
            temporarySizeCounter = 0;
        }
        if (selectionWidth == 3){
            temporarySizeCounter = 0;
        }
        else{
            temporarySizeCounter++;
        }
    }

    void BonusSquareClicked(int colorEnum){
        if (colorEnum == 7){
            if (selectionWidth == 1 || selectionWidth == 3){
                selectionWidth = selectionWidth + 2;
            }
        }
        else if (colorEnum == 8){
            if (selectionWidth == 3 || selectionWidth == 5){
                selectionWidth = selectionWidth - 2;
            }
        }
        else if (colorEnum == 9){
            LevelManager lm = GameObject.Find("LevelManager").GetComponent<LevelManager>();
            lm.moves = lm.moves + 3;
            AnimateMovesTextColor();
            mt.UpdateMovesText(movesText);
        }
    }

    void AnimateMovesTextColor(){
        //Triggers animation
        GameObject.Find("MovesText").GetComponent<Animation>().Play("MovesTextBonusAnim");
        //Invoke("ReadyMovesTextAnim", 1);
    }

    void ReadyMovesTextAnim(){
        GameObject.Find("MovesText").GetComponent<Animator>().SetBool("isGrey", false);
    }

}
    
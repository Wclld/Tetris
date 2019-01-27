using System.Collections;
using System.Linq.Expressions;
using UnityEngine;

public class BlocMover : MonoBehaviour // declaring class and inheriting it from MonoBehaviour, so this class can be attached to GameObject
{
    [SerializeField] // adding do field attribute, so it can be changed in Unity's inspector  
    private float blocStartMoveTime = 1.5f; // declaring public field with floating coma, that can be accessed from other class and changed through Unity's editor 
    [SerializeField]
    private float minMoveTime = 0.1f;
    [SerializeField] 
    private float currentMoveTime; // declaring private value, because this one is not needed in public access

    [SerializeField] 
    private float timeDecreasePercent = 0.01f;
    private Bloc currentBloc;
    private Coroutine movingCoroutine;
    private Coroutine moveTillDownCoroutine;
    private bool isCurrentBlocNull => currentBloc == null;

    private GameManager gameManager => GameManager.Instance;

    private void Start()
    {
        currentMoveTime = blocStartMoveTime;
        BindMethods();
    }

    private void BindMethods()
    {
        gameManager.OnGamePaused += Stop;
        gameManager.OnGameStarted += StartMoving;
        gameManager.InputManager.OnLeftPressed += MoveAside;
        gameManager.InputManager.OnRightPressed += MoveAside;
        gameManager.InputManager.OnUpPressed += Rotate;
        gameManager.InputManager.OnDownPressed += MoveTillBottom;
        gameManager.OnBlockStopped += SetBloc;
        gameManager.OnBlockStopped += () =>
        {
            if(moveTillDownCoroutine != null)
                StopCoroutine(moveTillDownCoroutine);
        };
        gameManager.OnGameOver += StopAllCoroutines;
        gameManager.OnGameOver += StopAllCoroutines;
    }

    public void SetBloc() // declaring a public method that can be accessed from other class
    {
        currentBloc = GameManager.Instance.BlocSpawner.SpawnBloc();
    }
    public void StartMoving()
    {
        movingCoroutine = StartCoroutine(MoveBlocDownRoutine()); // creating and assigning to field coroutine: a function that is executed in intervals and continues from last yield while it's not finished  
    }
    public void Stop()
    {
        if (movingCoroutine != null)// using conditional statement to check that value is not empty
        {
            StopCoroutine(movingCoroutine); // making Coroutine stop before it finishes its work
            currentBloc = null;
        }
    }
    
    private IEnumerator MoveBlocDownRoutine() // declaring function that cannot be easily accessed from other classes
    {
        while (true)
        {
            yield return new WaitForSeconds(currentMoveTime); // making this function to wait for declared time
            if (isCurrentBlocNull)
                SetBloc();
            MoveDown();
            currentMoveTime = DecreaseTime(currentMoveTime); // passing and getting value from local function
        }  
    }
    
    public void MoveAside(MoveSide side)
    {
       Move(new Vector3((float)side,0,0)); // invoking local Method passing to it new value
    }
    public void MoveDown()
    {
       Move(Vector3.down);
    }

    private void MoveTillBottom()
    {
        moveTillDownCoroutine = StartCoroutine(MoveTillBottomRoutine());
    }

    private IEnumerator MoveTillBottomRoutine()
    {
        while (true)
        {
            MoveDown();
            yield return null;
        }
    }

    public void Rotate()
    {
        currentBloc?.Rotate(); // checking for object is not null and calling accessible method
        if(!gameManager.Grid.BlocIsInGrid(currentBloc.transform))
            currentBloc?.Rotate(true); 
            
    }

    private void Move(Vector3 direction)
    {
        if (currentBloc != null)
        {
            currentBloc.transform.localPosition += direction;
            if (!gameManager.Grid.BlocIsInGrid(currentBloc.transform))
                currentBloc.transform.localPosition -= direction;
            if (gameManager.Grid.IsOnBottom(currentBloc.transform))
                gameManager.BlocStopped();
        }
        else
            Debug.LogError("Bloc is not set!"); // logging message th the unity console
    }
    private float DecreaseTime(float time)
    {
        var calculatedTime = time - (time * timeDecreasePercent); 

        if(calculatedTime > minMoveTime)
            calculatedTime = time;

        return calculatedTime; //returning value from function
    }

    private void FinishGame()
    {
        StopAllCoroutines();
        Destroy(currentBloc.gameObject);
    }

}
public enum MoveSide // declaring enumeration 
{
    Left = -1, // setting custom values
    Right = 1
}

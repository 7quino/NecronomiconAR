using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;

public class InputManager : MonoBehaviour
{
    public ARTrackedImageManager imageManager;
    Camera ARCamera;
    List<RaycastResult> rayCastResults = new List<RaycastResult>();
    bool introFinnished = false;
    bool gameOver = false;

    public UnityEvent<Ray> onSpawnGreens = new UnityEvent<Ray>();
    public UnityEvent onGreenMonsterPressed = new UnityEvent();
    public static InputManager Instance;

    private void Awake()
    {
        Instance = this;
        ARCamera = Camera.main;
    }

    private void Start()
    {
        NecronomiconGameManager.instance.onGameOver.AddListener(() => { gameOver = true; });
        imageManager.trackedImagesChanged += (ARTrackedImagesChangedEventArgs eventArgs) => { GreenMonster.instance.onIntroFinnished.AddListener(() => { introFinnished = true; }); };
    }

    void Update()
    {
        //Reading user inputs as finger pressing on screen, return if user not pressing on screen
        if (!Input.GetMouseButtonDown(0) || gameOver) return;

        Ray ray = ARCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitObject;

        if (!IsPointerOverUI(Input.mousePosition) && introFinnished) onSpawnGreens.Invoke(ray);
        if (Physics.Raycast(ray, out hitObject)) if (hitObject.transform.CompareTag("GreenMonster")) onGreenMonsterPressed.Invoke();
    }

    private bool IsPointerOverUI(Vector2 fingerPosition)
    {
        PointerEventData eventDataPosition = new PointerEventData(EventSystem.current);
        eventDataPosition.position = fingerPosition;

        //The list holds more than zero elements if finger is above UI element
        EventSystem.current.RaycastAll( eventDataPosition, rayCastResults);
        return rayCastResults.Count > 0;
    }
}



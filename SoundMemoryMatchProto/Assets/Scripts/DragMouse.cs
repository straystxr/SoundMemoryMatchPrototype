using UnityEngine;

public class DragMouse : MonoBehaviour
{

    public int noteIndex;
    public RandomNoteGenerator noteGenerator;

    private Vector2 mousePosition;

    private float offsetx;
    private float offsety;

    //when the button is not pressed 
    public bool mouseButtonReleased;

    private void OnMouseDown()
    {
        //button is pressed
        mouseButtonReleased = false;
        //getting the exact coordinates of where the pouse is 
        offsetx = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
        offsety = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;
        Debug.Log(offsetx + " " + offsety);

        //noteGenerator.OnNoteClicked(noteIndex);

    }

    private void OnMouseDrag()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(mousePosition.x -  offsetx, mousePosition.y - offsety);
    }

    private void OnMouseUp()
    {
        mouseButtonReleased = true;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

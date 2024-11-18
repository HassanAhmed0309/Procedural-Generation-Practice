using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float CharacterSpeed = 0f;
    [SerializeField] GameObject Stackpoint;
    List<Transform> stackedObjects = new List<Transform>();
    public Vector2 rateRange = new Vector2(0.8f, 0.8f);
    public float offset = 0.1f;

    public float xFrequency = 4.1f;
    public float xAmplitude = 1.0f;
    public float yFrequency = 4.3f;
    public float yAmplitude = 1.0f;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            gameObject.transform.position += new Vector3(0f, 0f, CharacterSpeed * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.D))
        {
            gameObject.transform.position += new Vector3(CharacterSpeed * Time.deltaTime, 0f, 0f);
        }
        if (Input.GetKey(KeyCode.A))
        {
            gameObject.transform.position -= new Vector3(CharacterSpeed * Time.deltaTime, 0f, 0f);
        }
        if (Input.GetKey(KeyCode.S))
        {
            gameObject.transform.position -= new Vector3(0f, 0f, CharacterSpeed * Time.deltaTime);
        }

        for(int i = 0; i < stackedObjects.Count; i++)
        {
           stackedObjects[i].localPosition = new Vector3(
               Mathf.Cos(Time.time * xFrequency) * xAmplitude,
               i * 0.4f,
               Mathf.Cos(Time.time * yFrequency) * yAmplitude
           );
        }   

        //for (int i = 1; i < stackedObjects.Count; ++i)
        //{
        //    float rate = Mathf.Lerp(rateRange.x, rateRange.y, (float)i / (float)stackedObjects.Count);
        //    stackedObjects[i].localPosition = Vector3.Lerp(stackedObjects[i].localPosition, stackedObjects[i - 1].localPosition + (stackedObjects[i - 1].up * offset), rate);
        //    stackedObjects[i].localRotation = Quaternion.Lerp(stackedObjects[i].localRotation, stackedObjects[i - 1].localRotation, rate);
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Pickable")
        {
            Transform thisTransform = other.gameObject.transform;
            thisTransform.SetParent(Stackpoint.transform);
            thisTransform.localPosition = new Vector3(0f, stackedObjects.Count * 0.4f, 0f);
            thisTransform.localRotation = Quaternion.Euler(0f, 0f, 90f);
            stackedObjects.Add(thisTransform);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ovni : MonoBehaviour
{
    public float endAttack;
    
    [SerializeField] private float m_interpolationValue = 0.25f;

    private Coroutine currentState;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("I'm in the state: " + currentState);
    }

    // Update is called once per frame
    void OnEnable()
    {
        SwitchState(IdleState());
    }

    //private void Update()
    //{
    //    Vector3 a = transform.position;
    //    Vector3 b = transform.position;
    //    float t = m_interpolationValue;

    //    m_movement.position = Vector3.Lerp(a, b, t);
    //}

    private IEnumerator IdleState()
    {
        Debug.Log("Processing...");

        yield return new WaitForSeconds(2);

        SwitchState(MovingState());
    }

    private IEnumerator MovingState()
    {
            Vector3 a = transform.position;
            Vector3 b = a + new Vector3(Random.value, 0, Random.value);

        for (float i = 0; i < m_interpolationValue; i += Time.deltaTime)
        {
            float t = m_interpolationValue;

            transform.position = Vector3.Lerp(a, b, t);
           
        }

        yield return null;

        SwitchState(AttackingState());
    }
    private IEnumerator AttackingState()
    {
        Debug.Log("Attacking!");

        yield return new WaitForSeconds(0.5f);

        SwitchState(IdleState());
    }

    private void SwitchState(IEnumerator state)
    {
        if (currentState != null) StopCoroutine(currentState);

        if (state == null) return;

        currentState = StartCoroutine(state);
    }
}

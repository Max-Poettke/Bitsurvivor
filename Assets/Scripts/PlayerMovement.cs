using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    public TMP_Text interactionTextPrefab;
    public GameObject TowerMenu;
    public Canvas canvas;
    private bool nearTower = false;

    private TMP_Text textInstance;
    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float verticalInput = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        transform.position += new Vector3(horizontalInput, verticalInput, 0);
        if (Input.GetKeyDown(KeyCode.E))
        {
            EPressed();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Tower"))
        {
            textInstance = Instantiate(interactionTextPrefab, canvas.transform);
            nearTower = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Tower"))
        {
            if (textInstance.IsUnityNull()) return;
            Destroy(textInstance.gameObject);
            TowerMenu.SetActive(false);
            textInstance = null;
            nearTower = false;
        }
    }

    void EPressed()
    {
        if (nearTower)
        {
            if (textInstance.IsUnityNull()) return;
            textInstance.gameObject.SetActive(!textInstance.gameObject.activeSelf);
            TowerMenu.SetActive(!TowerMenu.activeSelf);
        }
    }
    
}

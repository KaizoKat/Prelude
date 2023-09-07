using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class DITOCHOME : MonoBehaviour
{
    // Display Info Text On Cursor Hover Over Map Element

    private Camera _cam;
    private RaycastHit _out;

    [SerializeField] private ControllsHandler _ch;
    [SerializeField] private GameObject ui_descriptionHolder;
    [SerializeField] private TMP_Text ui_DHT_TITLE;
    [SerializeField] private TMP_Text ui_DHT_REST;
    [SerializeField] private List<Descriptor> DCPT;

    Texture tex;
    Color cc;

    void Start()
    {
        _cam = GetComponent<Camera>();
    }

    void Update()
    {
        ui_descriptionHolder.transform.position = Vector3.Lerp(ui_descriptionHolder.transform.position, Input.mousePosition, Time.deltaTime * 16);

        if (true) //while map is on.
        {
            if (Physics.Raycast(_cam.ScreenPointToRay(Input.mousePosition/2), out _out,10))
            {
                if (Input.GetKeyDown(_ch.i_mouse_left))
                {
                    if (ui_descriptionHolder.transform.GetChild(0).gameObject.activeInHierarchy == false)
                        ui_descriptionHolder.transform.GetChild(0).gameObject.SetActive(true);
                    tex = _out.transform.GetComponentInChildren<MeshRenderer>().material.mainTexture;
                    cc = _out.transform.GetComponentInChildren<MeshRenderer>().material.color;

                    ClickEffectDown(_out.transform.GetComponentInChildren<MeshRenderer>().material);
                } 
                else if (Input.GetKeyUp(_ch.i_mouse_left))
                    ClickEffectUp(_out.transform.GetComponentInChildren<MeshRenderer>().material);

                for (int i = 0; i < DCPT.Count; i++)
                {
                    if (_out.transform.gameObject.tag == DCPT[i].Tag)
                    {
                        if (DCPT[i].is_BOSSROOM)
                        {
                            ui_DHT_TITLE.text = DCPT[i].Name;
                            ui_DHT_REST.text = DCPT[i].Description;
                            /*
                            "Enemys =" + DCPT[i].EnemyCount + "\n" +
                            "Avilable Loot =" + DCPT[i].LootCount + "\n" +
                            "Closed Doors =" + DCPT[i].LootCount + "\n" +
                            "Boss room!";
                            */
                        }
                        else if (DCPT[i].is_CORIDOR)
                        {
                            ui_DHT_TITLE.text = DCPT[i].Name;
                            ui_DHT_REST.text = DCPT[i].Description;
                        }
                        else if (DCPT[i].is_HAS_SECRETS)
                        {
                            ui_DHT_TITLE.text = DCPT[i].Name;
                            ui_DHT_REST.text = DCPT[i].Description;
                            /*
                            "Enemys =" + DCPT[i].EnemyCount + "\n" +
                            "Avilable Loot =" + DCPT[i].LootCount + "\n" +
                            "Avilable Secrets =" + DCPT[i].SecretsCount + "\n" +
                            "Closed Doors =" + DCPT[i].LootCount + "\n";
                            */
                        }
                        else
                        {
                            ui_DHT_TITLE.text = DCPT[i].Name;
                            ui_DHT_REST.text = DCPT[i].Description;
                            /*
                            "Enemys =" + DCPT[i].EnemyCount + "\n" +
                            "Avilable Loot =" + DCPT[i].LootCount + "\n" +
                            "Closed Doors =" + DCPT[i].LootCount;
                            */
                        }

                    }
                }
            }
            else
            {
                if (ui_descriptionHolder.transform.GetChild(0).gameObject.activeInHierarchy == true)
                    ui_descriptionHolder.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
        else
        {
            if (ui_descriptionHolder.transform.GetChild(0).gameObject.activeInHierarchy == true)
                ui_descriptionHolder.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    void ClickEffectDown(Material _effectO)
    {

        _effectO.mainTexture = null;
        _effectO.color = Color.white;
    }

    void ClickEffectUp(Material _effectO)
    {
        _effectO.mainTexture = tex;
        _effectO.color = cc;
    }
}

[System.Serializable]
public class Descriptor
{
    public string Name = "";
    public string Description = "";
    public bool is_CORIDOR;
    public bool is_BOSSROOM;
    public bool is_HAS_SECRETS;
    public string Tag;
}

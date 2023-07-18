using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HealthBarUIController : MonoBehaviour
{
    public static HealthBarUIController Instance;
    // Start is called before the first frame update
    [SerializeField]
    private Image _healthBarPrefab;
    private Dictionary<Player, Image> _healthBars = new Dictionary<Player, Image>();
    private Camera _camera;
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        _camera = Camera.main; 
    }

    public void CreateHealthBar(Player target)
    {
        Image image = Instantiate(_healthBarPrefab, this.transform);
        image.transform.position = _camera.WorldToScreenPoint(target.transform.position) + new Vector3(0, 40, 0);
        image.GetComponentInChildren<TextMeshProUGUI>().text = target.photonView.Owner.NickName;
        target.OnHealthChanged.AddListener(OnHealthChanged);
        target.OnPlayerDestroyed.AddListener(OnPlayerRemoved);
        _healthBars.Add(target, image);
        //create ui

    }
    private void OnHealthChanged(Player player)
    {
        _healthBars[player].transform.GetChild(0).GetComponent<Image>().fillAmount = (float)player.Health / (float)player.MaxHealth;
    }
    private void OnPlayerRemoved(Player player)
    {
        Destroy(_healthBars[player].gameObject);
        _healthBars.Remove(player);
    }
    private void Update()
    {
        UpdatePosition();
    }
    private void UpdatePosition()
    {
        foreach(Player player in _healthBars.Keys)
        {
            _healthBars[player].transform.position = _camera.WorldToScreenPoint(player.transform.position);
        }
    }
}

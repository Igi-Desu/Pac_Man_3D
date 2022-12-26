using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages number of pellets
/// </summary>
public class pelletcounter : Singleton<pelletcounter>
{
    int pelletCount=0;
    [SerializeField]
    Text pelletCountText;
    /// <summary>
    /// get or set current amount of pellets needed to win game
    /// </summary>
    /// <value></value>
    public int PelletCount { get { return pelletCount; } set
        {
            pelletCountText.text = value.ToString();
            pelletCount = value;
        } }
}

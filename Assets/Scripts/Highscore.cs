using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class Highscore : MonoBehaviour
{
    #region Editor Variables
    [SerializeField]
    [Tooltip("The text component that is displaying the score. The text value " +
       "of this component will change with the score.")]
    private Text m_UIText;
    #endregion

    #region Non-Editor Variables
    public int m_Score;
    #endregion

    #region Singletons
    private static Highscore st;
    #endregion

    #region First Time Initialization and Set Up
    private void Awake()
    {
        st = this;
    }

    public void Start()
    {
        m_Score = 0;
        SetScore(0);
    }
    #endregion

    #region Accessors and Mutators
    public static Highscore Singleton
    {
        get { return st; }
    }
    #endregion

    #region Score Modification Methods
    public void SetScore(int score)
    {
        m_Score += score;
        m_UIText.text = "High: " + m_Score;
    }
    #endregion
}

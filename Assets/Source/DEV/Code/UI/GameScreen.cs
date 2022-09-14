using Kuhpik;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScreen : UIScreen
{
    [Header("Jetpack UI")]
    [HorizontalLine(color: EColor.Blue)]
    [SerializeField] private Slider jetpackFuelFill;
    [SerializeField] private Color emptyFuelColor;
    [SerializeField] private Image jetpackFuelIcon;

    [Header("Health UI")]
    [HorizontalLine(color: EColor.Red)]
    [SerializeField] private Image healthIndicator;

    public Slider JetpackFuelFill => jetpackFuelFill;
    public Color EmptyFuelColor => emptyFuelColor;
    public Image JetpackFuelIcon => jetpackFuelIcon;
    public Image HealthIndicator => healthIndicator;
}

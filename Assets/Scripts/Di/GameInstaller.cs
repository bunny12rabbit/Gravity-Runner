using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public GameController gameController;
    public Player player;
    public InputController inputController;
    public UIController uIController;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<GameController>().FromInstance(gameController).AsSingle();
        Container.BindInterfacesAndSelfTo<Player>().FromInstance(player).AsSingle();
        Container.BindInterfacesAndSelfTo<InputController>().FromInstance(inputController).AsSingle();
        Container.BindInterfacesAndSelfTo<ScoreSystem>().AsSingle();
        Container.BindInterfacesAndSelfTo<DataManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<UIController>().FromInstance(uIController).AsSingle();
    }
}
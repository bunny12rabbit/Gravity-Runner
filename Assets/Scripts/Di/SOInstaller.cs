using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "SOInstaller", menuName = "Installers/SOInstaller")]
public class SOInstaller : ScriptableObjectInstaller<SOInstaller>
{
    public GameConfig config;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<GameConfig>().FromInstance(config).AsSingle();
    }
}
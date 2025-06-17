using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public StageManager StageManager { get; private set; }
    public QuotaManager QuotaManager { get; private set; }
    public GoldManager GoldManager { get; private set; }
    public UpgradeManager UpgradeManager { get; private set; }

    private void Awake()
    {
        Init();
    }
    private void Init()
    {
        SingletonInit();

        StageManager = GetComponentInChildren<StageManager>();
        QuotaManager = GetComponentInChildren<QuotaManager>();
        GoldManager = GetComponentInChildren<GoldManager>();
        UpgradeManager = GetComponentInChildren<UpgradeManager>();

        QuotaManager.Init(StageManager);
    }
}

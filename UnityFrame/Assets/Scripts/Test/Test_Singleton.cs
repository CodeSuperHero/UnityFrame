//CodeSuperHero 20150602

using UnityEngine;
using System.Collections;
using CodeSuperHero.UF;

public class Test_Singleton : MonoBehaviour
{
    Test_SingletonMono test_smono;
    ILog test_singletonLog;
    void Start()
    {
        test_singletonLog = LogFactory.GetLog("test_singletonLog", Log.LOG_COLOR_GREEN);

        test_smono = Test_SingletonMono.instance;
        test_smono.Func();
        test_smono.Free();
        StartCoroutine(CheckExist());
        Test_SingletonClass test_sclass = Test_SingletonClass.instance;
        test_sclass.Func();
        test_sclass.Free();
    }

    IEnumerator CheckExist()
    {
        yield return 1;
        test_singletonLog.LogNormal("LogNormal CheckExist" + test_smono);
        test_singletonLog.LogNormal("LogNormal CheckExist : {0}." , test_smono);
        test_singletonLog.LogWarning("LogNormal CheckExist" + test_smono);
        test_singletonLog.LogWarning("LogNormal CheckExist : {0}.", test_smono);
        test_singletonLog.LogError("LogNormal CheckExist" + test_smono);
        test_singletonLog.LogError("LogNormal CheckExist : {0}.", test_smono);

        test_singletonLog.output = false;
        test_singletonLog.LogNormal("LogNormal CheckExist" + test_smono);
        test_singletonLog.LogNormal("LogNormal CheckExist : {0}.", test_smono);
        test_singletonLog.LogWarning("LogNormal CheckExist" + test_smono);
        test_singletonLog.LogWarning("LogNormal CheckExist : {0}.", test_smono);
        test_singletonLog.LogError("LogNormal CheckExist" + test_smono);
        test_singletonLog.LogError("LogNormal CheckExist : {0}.", test_smono);
    }
}

public class Test_SingletonMono : SingletonMono<Test_SingletonMono>
{
    ILog log;
    public void Func()
    {
        log.LogWarning("Test_SingletonMono Excute Func" + this);
        log.LogWarning("Test_SingletonMono Excute Func ： {0}.", this);
    }

    protected override void Init()
    {
        log = DebugLog.instance;
        log.LogNormal(" SingletonMono Init " + this);
        log.LogNormal(" SingletonMono Init ： {0}", this);
    }

    public override void Free()
    {
        Destroy(this);
        _instance = null;
        log.LogError(" SingletonMono Free " + this);
        log.LogError(" SingletonMono Free {0},", this);
        Test_SingletonMono tsm = singletonObjec.GetComponent<Test_SingletonMono>();
        log.LogError(" tsm : " + tsm);

        log.output = false;
    }
}

public class Test_SingletonClass : Singleton<Test_SingletonClass>
{
    public Test_SingletonClass()
    {
        Debug.LogError("Test_SingletonClass Constructed Function");
    }

    public void Func()
    {
        Debug.LogError("Test_SingletonClass Excute Func");
    }

    public override void Free()
    {
        _instance = null;
        Debug.LogError("Test_SingletonClass Free Function");
    }
}
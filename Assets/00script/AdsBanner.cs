using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdsBanner : MonoBehaviour {

	public bool m_bShown;
#if UNITY_ANDROID
	private string UNIT_ID = "ca-app-pub-5869235725006697/5028217561";
#elif UNITY_IPHONE
	private string UNIT_ID = "ca-app-pub-5869235725006697/4888616762";
#endif
	BannerView m_bannerView;

	private bool m_bIsTate;
	private bool m_bIsTatePre;

	private string m_strPageName;

	// Use this for initialization
	void Awake () {
		m_bShown = false;
		m_bannerView = null;

		m_bIsTate = Screen.height > Screen.width ? true : false;
		m_bIsTatePre = !m_bIsTate;

		UIAssistant.onScreenResize += OnScreenResize;
		UIAssistant.onShowPage += OnShowPage;

		show();

		// なにもこんなところでやらなくてもいいんだけど
		Application.targetFrameRate = 60;

	}

	private void OnScreenResize()
	{
		OnShowPage(m_strPageName);
	}

	public void show()
	{
		m_bIsTate = Screen.height > Screen.width ? true : false;

		if(m_bIsTatePre != m_bIsTate)
		{
			if (m_bannerView != null)
			{
				m_bannerView.Destroy();
			}
			AdPosition p = AdPosition.Bottom;
			if( m_bIsTate == false)
			{
				p = AdPosition.BottomRight;
			}
			m_bannerView = new BannerView(UNIT_ID, AdSize.Banner, p);
			AdRequest request = new AdRequest
				.Builder()
				.AddTestDevice("30ec665ef7c68238905003e951174579")
				.AddTestDevice("B58A62380C00BF9DC7BA75C756B5F550").Build();
			m_bannerView.LoadAd(request);
		}
		else
		{
			m_bannerView.Show();
		}
		m_bIsTatePre = m_bIsTate;
	}
	public void hide()
	{
		if (m_bannerView != null)
		{
			m_bannerView.Hide();
		}
	}

	private void OnShowPage( string _strName)
	{
		switch (_strName)
		{
			case "Booster":
				hide();
				break;
			default:
				show();
				break;
		}
		m_strPageName = _strName;
	}

}

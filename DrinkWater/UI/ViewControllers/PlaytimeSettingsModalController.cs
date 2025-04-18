﻿using System.Reflection;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Parser;
using DrinkWater.Configuration;
using UnityEngine;

namespace DrinkWater.UI.ViewControllers
{
	internal sealed class PlaytimeSettingsModalController
	{
		private bool _parsed;
		private DrinkWaterSettingsViewController.SetSettingsButtonUnderline _setSettingsButtonUnderlineDelegate = null!;
		
		[UIParams] private readonly BSMLParserParams _parserParams = null!;
		
		[UIValue("enable-playtime-bool")]
		private bool EnableByPlaytimeValue
		{
			get => _pluginConfig.EnableByPlaytime;
			set
			{
				_pluginConfig.EnableByPlaytime = value;
				_setSettingsButtonUnderlineDelegate(value);
			}
		}

		[UIValue("playtime-warning-int")]
		private int PlaytimeBeforeWarningValue
		{
			get => _pluginConfig.PlaytimeBeforeWarning;
			set => _pluginConfig.PlaytimeBeforeWarning = value;
		}

		[UIValue("count-pause-time-bool")]
		private bool CountPauseTimeValue
		{
			get => _pluginConfig.CountPauseTime;
			set => _pluginConfig.CountPauseTime = value;
		}
		
		private readonly PluginConfig _pluginConfig;

		public PlaytimeSettingsModalController(PluginConfig pluginConfig)
		{
			_pluginConfig = pluginConfig;
		}
		
		[UIAction("format-playtime-slider")]
		private string FormatPlaytimeSlider(int value)
		{
			if (value == 1)
			{
				return "1 minute";
			}

			return value + " minutes";
		}

		private void Parse(Component parentTransform, DrinkWaterSettingsViewController.SetSettingsButtonUnderline setSettingsButtonUnderlineDelegate)
		{
			if (!_parsed)
			{
				BSMLParser.Instance.Parse(Utilities.GetResourceContent(Assembly.GetExecutingAssembly(), "DrinkWater.UI.Views.PlaytimeSettingsView.bsml"), parentTransform.gameObject, this);
				_setSettingsButtonUnderlineDelegate = setSettingsButtonUnderlineDelegate;
				_parsed = true;
			}
		}

		public void ShowModal(Transform parentTransform,  DrinkWaterSettingsViewController.SetSettingsButtonUnderline setSettingsButtonUnderlineDelegate)
		{
			Parse(parentTransform, setSettingsButtonUnderlineDelegate);
			
			_parserParams.EmitEvent("close-modal");
			_parserParams.EmitEvent("open-modal");
		}
	}
}
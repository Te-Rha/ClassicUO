﻿using System;
using System.Collections.Generic;
using System.Text;
using ClassicUO.Configuration;
using ClassicUO.Game.Gumps.Controls;
using ClassicUO.IO.Resources;
using ClassicUO.Renderer;

namespace ClassicUO.Game.Gumps.UIGumps
{
    public class OptionsGump : Gump
    {

        private enum Buttons
        {
            SoundAndMusic,
            Configuration,
            Language,
            Chat,
            Macro,
            Interface,
            Display,
            Reputation,
            Misc,
            FilterOptions,

            Cancel,
            Apply,
            Default,
            Ok

        }

        private readonly Settings _settings;
        private HSliderBar _sliderSound, _sliderMusic, _sliderFPS;

        public OptionsGump() : base(0, 0)
        {
            CanMove = true;
            AcceptMouseInput = false;

            _settings = Service.Get<Settings>();

            // base
            AddChildren(new ResizePic(0x0A28){ X = 40, Y = 0, Width = 550, Height = 450});

            // left
            AddChildren(new Button((int)Buttons.SoundAndMusic, 0x00DA, 0x00DA) { X= 0,  Y = 45, ButtonAction =  ButtonAction.SwitchPage, ButtonParameter = 1});
            AddChildren(new Button((int)Buttons.Configuration, 0x00DC, 0x00DC) { X = 0, Y = 111, ButtonAction = ButtonAction.SwitchPage, ButtonParameter = 2});
            AddChildren(new Button((int)Buttons.Language, 0x00DE, 0x00DE) { X = 0, Y = 177, ButtonAction = ButtonAction.SwitchPage, ButtonParameter = 3 });
            AddChildren(new Button((int)Buttons.Chat, 0x00E0, 0x00E0) { X = 0, Y = 243, ButtonAction = ButtonAction.SwitchPage, ButtonParameter = 4});
            AddChildren(new Button((int)Buttons.Macro, 0x00ED, 0x00ED) { X = 0, Y = 309, ButtonAction = ButtonAction.SwitchPage, ButtonParameter = 5});

            // right
            AddChildren(new Button((int)Buttons.Interface, 0x00E2, 0x00E2) { X = 576, Y = 45,  ButtonAction = ButtonAction.SwitchPage, ButtonParameter = 6 });
            AddChildren(new Button((int)Buttons.Display, 0x00E4, 0x00E4) { X = 576, Y = 111, ButtonAction = ButtonAction.SwitchPage, ButtonParameter = 7});
            AddChildren(new Button((int)Buttons.Reputation, 0x00E6, 0x00E6) { X = 576, Y = 177, ButtonAction = ButtonAction.SwitchPage, ButtonParameter = 8});
            AddChildren(new Button((int)Buttons.Misc, 0x00E8, 0x00E8) { X = 576, Y = 243, ButtonAction = ButtonAction.SwitchPage, ButtonParameter = 9});
            AddChildren(new Button((int)Buttons.FilterOptions, 0x00EB, 0x00EB) { X = 576, Y = 309, ButtonAction = ButtonAction.SwitchPage, ButtonParameter = 10});

            // bottom
            AddChildren(new Button((int)Buttons.Cancel, 0x00F3, 0x00F1, 0x00F2) { X = 154, Y = 405, ButtonAction = ButtonAction.Activate, ButtonParameter = 0});
            AddChildren(new Button((int)Buttons.Apply, 0x00EF, 0x00F0, 0x00EE) { X = 248, Y = 405, ButtonAction = ButtonAction.Activate, ButtonParameter = 0 });
            AddChildren(new Button((int)Buttons.Default, 0x00F6, 0x00F4, 0x00F5) { X = 346, Y = 405, ButtonAction = ButtonAction.Activate, ButtonParameter = 0});
            AddChildren(new Button((int)Buttons.Ok, 0x00F9, 0x00F8, 0x00F7) { X = 443, Y = 405, ButtonAction = ButtonAction.Activate, ButtonParameter = 0 });

            BuildPage1();
            BuildPage2();
            BuildPage3();
            BuildPage4();
            BuildPage5();
            BuildPage6();
            BuildPage7();
            BuildPage8();
            BuildPage9();
            BuildPage10();

            ChangePage(2);
        }


        private void BuildPage1()
        {
            AddChildren(new GumpPic(0, 45, 0x00D9, 0) { CanMove = false }, 1);

            Label label = new Label("Sound and Music", true, 0, 460, align: TEXT_ALIGN_TYPE.TS_CENTER)
            {
                X = 84,
                Y = 22
            };
            AddChildren(label, 1);

            label = new Label("These settings affect the sound and music you will hear while playing Ultima Online.",
                true, 0, maxwidth: 500)
            {
                X = 64,
                Y = 44
            };
            AddChildren(label, 1);

            Checkbox checkbox = new Checkbox(0x00D2, 0x00D3, "Sound On/Off")
            {
                X = 64, Y = 90,
                IsChecked = _settings.Sound,                
            };
            AddChildren(checkbox, 1);

            label = new Label("Sound Volume", true, 0) { X= 64, Y = 112 };
            AddChildren(label, 1);

            _sliderSound = new HSliderBar(64, 133, 90, 0, 255, _settings.SoundVolume, HSliderBarStyle.MetalWidgetRecessedBar, true);
            AddChildren(_sliderSound, 1);

            checkbox = new Checkbox(0x00D2, 0x00D3, "Music On/Off")
            {
                X = 64, Y = 151, IsChecked = _settings.Music
            };
            AddChildren(checkbox, 1);

            label = new Label("Music volume", true, 0) { X= 64, Y = 173};
            AddChildren(label, 1);

            _sliderMusic = new HSliderBar(64, 194, 90, 0, 255, _settings.MusicVolume, HSliderBarStyle.MetalWidgetRecessedBar, true);
            AddChildren(_sliderMusic, 1);

            checkbox = new Checkbox(0x00D2, 0x00D3, "Play footsteps sound")
            {
                X = 64, Y = 212
            };
            AddChildren(checkbox, 1);

            checkbox = new Checkbox(0x00D2, 0x00D3, "Play combat music")
            {
                X = 64,
                Y = 232
            };
            AddChildren(checkbox, 1);

            checkbox = new Checkbox(0x00D2, 0x00D3, "Play sounds in background")
            {
                X = 64,
                Y = 252
            };
            AddChildren(checkbox, 1);
        }

        private void BuildPage2()
        {
            AddChildren(new GumpPic(0, 111, 0x00DB, 0) { CanMove = false }, 2);

            Label label = new Label("ClassicUO configuration", true, 0, 460, align: TEXT_ALIGN_TYPE.TS_CENTER)
            {
                X = 84, Y = 22
            };
            AddChildren(label, 2);


            HtmlGump scrollArea = new HtmlGump(64, 90, 500, 300, false, true);
            AddChildren(scrollArea, 2);


            label = new Label("FPS:", true, 0)
            {
                X = 0,
                Y = 0
            };
            scrollArea.AddChildren(label);

            _sliderFPS = new HSliderBar(0, 21, 90, 15, 250, _settings.MaxFPS, HSliderBarStyle.MetalWidgetRecessedBar, true);
            scrollArea. AddChildren(_sliderFPS);
        }

        private void BuildPage3()
        {
            AddChildren(new GumpPic(0, 177, 0x00DD, 0) { CanMove = false }, 3);


        }

        private void BuildPage4()
        {
            AddChildren(new GumpPic(0, 243, 0x00DF, 0) { CanMove = false }, 4);

        }

        private void BuildPage5()
        {
            AddChildren(new GumpPic(0, 309, 0x00EC, 0) { CanMove = false }, 5);

        }

        private void BuildPage6()
        {
            AddChildren(new GumpPic(576, 45, 0x00E1, 0) { CanMove = false }, 6);

        }

        private void BuildPage7()
        {
            AddChildren(new GumpPic(576, 111, 0x00E3, 0) { CanMove = false }, 7);

        }

        private void BuildPage8()
        {
            AddChildren(new GumpPic(576, 177, 0x00E5, 0) { CanMove = false }, 8);

        }

        private void BuildPage9()
        {
            AddChildren(new GumpPic(576, 243, 0x00E7, 0) { CanMove = false }, 9);

        }

        private void BuildPage10()
        {
            AddChildren(new GumpPic(576, 309, 0x00EA, 0) { CanMove = false }, 10);

        }

        public override void OnButtonClick(int buttonID)
        {

        }
    }
}

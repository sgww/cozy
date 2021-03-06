﻿using System;
using CocosSharp;
using CozyAdventure.Public.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyAdventure.Game.Object;
using CozyAdventure.Model;
using CozyAdventure.View.Sprite;
using CozyAdventure.Game.Logic;
using CozyAdventure.Game.Manager;
using Cozy.Game.Manager;

namespace CozyAdventure.View.Layer
{
    public class FollowerListUiLayer : CCLayer
    {
        private FollowerCollect FollowerList { get; set; }

        private int Page { get; set; }

        private int CurPage { get; set; }

        private List<FollowerSprite> SpriteList { get; set; } = new List<FollowerSprite>();

        private CCLabel AllFollower { get; set; }

        private CCLabel PageNumber { get; set; }

        private CozySampleButton LastPageButton { get; set; }

        private CozySampleButton NextPageButton { get; set; }

        private CozySampleListView[] InnerList { get; set; }

        private FollowerDetailSprite ShowDetail { get; set; }

        private ButtonEventDispatcher dispatcher { get; set; } = new ButtonEventDispatcher();
        private ButtonEventDispatcher uidispatcher { get; set; } = new ButtonEventDispatcher();

        protected override void AddedToScene()
        {
            base.AddedToScene();
            InitUI();
        }

        public override void OnEnter()
        {
            base.OnEnter();
            RefreshPage();
            MessageManager.RegisterMessage("Message.FollowerFight.Success", OnFightStatusSwitch);
            dispatcher.AttachListener(this);
            uidispatcher.AttachListener(this);
        }

        public override void OnExit()
        {
            base.OnExit();
            MessageManager.UnRegisterMessage("Message.FollowerFight.Success", OnFightStatusSwitch);
            dispatcher.DetachListener(this);
            uidispatcher.DetachListener(this);
        }

        #region UI

        private void InitUI()
        {
            var listlable = new CCLabel("佣兵列表", StringManager.GetText("GlobalFont"), 14)
            {
                Position    = new CCPoint(100, 420),
                Color       = CCColor3B.White
            };

            FollowerList    = PlayerObject.Instance.Self.AllFollower;
            Page            = (FollowerList.Followers.Count + 8) / 9;

            AddChild(listlable, 100);

            foreach (var obj in FollowerList.Followers)
            {
                var fs = new FollowerSprite(obj, true);
                this.AddChild(fs);
                SpriteList.Add(fs);
            }

            var listview = new CozySampleListView()
            {
                ContentSize = new CCSize(600, 330),
                HasBorder   = true,
                Position    = new CCPoint(100, 60)
            };
            this.AddChild(listview);

            InnerList = new CozySampleListView[3];
            for (int i = 0; i < 3; ++i)
            {
                InnerList[i] = new CozySampleListView()
                {
                    ContentSize = new CCSize(200, 330),
                    Orientation = Public.Controls.Enum.ControlOrientation.Vertical,
                };
                listview.AddItem(new CozySampleListViewItem(InnerList[i]));
            }

            int fight = PlayerObject.Instance.Self.FightFollower.Followers.Count;
            AllFollower = new CCLabel("总数" + fight + "/" + FollowerList.Followers.Count, StringManager.GetText("GlobalFont"), 14)
            {
                Position    = new CCPoint(92, 37),
                Color       = CCColor3B.White,
            };
            AddChild(AllFollower, 100);

            PageNumber = new CCLabel((CurPage + 1) + "/" + Page, StringManager.GetText("GlobalFont"), 14)
            {
                Position    = new CCPoint(389, 37),
                Color       = CCColor3B.White
            };
            AddChild(PageNumber, 100);

            ShowDetail = new FollowerDetailSprite()
            {
                Position    = new CCPoint(100, 100),
                AnchorPoint = CCPoint.Zero,
                Visible     = false,
                FightStatusChangeCallback = new Action<Follower>(OnStatusChange),
            };
            this.AddChild(ShowDetail, 201);

            LastPageButton = new CozySampleButton(473, 17, 78, 36)
            {
                Text        = "上一页",
                FontSize    = 14,
                OnClick     = () => PrevPage(),
            };
            NextPageButton = new CozySampleButton(585, 17, 78, 36)
            {
                Text        = "下一页",
                FontSize    = 14,
                OnClick     = () => NextPage(),
            };
            AddChild(NextPageButton, 100);
            AddChild(LastPageButton, 100);
            uidispatcher.Add(NextPageButton);
            uidispatcher.Add(LastPageButton);

            var backButton = new CozySampleButton(650, 17, 78, 36)
            {
                Text = "返回",
                FontSize = 14,
                OnClick = () =>
                {
                    AppDelegate.SharedWindow.DefaultDirector.PopScene();
                }
            };
            AddChild(backButton, 100);
            uidispatcher.Add(backButton);
        }

        #endregion

        private void PatternFollower()
        {
            int count = CurPage * 9;
            for (int i = 0; i < 3; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    if (count < FollowerList.Followers.Count)
                    {
                        int index = count;

                        var item = new CozySampleListViewItem(SpriteList[index])
                        {
                            MarginTop = 5,
                            MarginBottom = 5,
                        };

                        var button = new CozySampleButton(item.ContentSize.Width, item.ContentSize.Height)
                        {
                            OnClick = () =>
                            {
                                if (!ShowDetail.Visible)
                                {
                                    ShowDetail.CurrFollower = SpriteList[index].BindFollower;
                                    ShowDetail.Visible = true;
                                }
                            },
                        };
                        item.AddChild(button);
                        dispatcher.Add(button);

                        SpriteList[index].Visible = true;
                        InnerList[i].AddItem(item);
                    }
                    count++;
                }
            }
        }

        private void ResetUIElement()
        {
            foreach (var obj in SpriteList)
            {
                obj.Visible = false;
            }
            foreach (var list in InnerList)
            {
                list.Clear();
            }
            dispatcher.Clear();
        }

        private void RefreshPage()
        {
            ResetUIElement();
            PatternFollower();

            PageNumber.Text = (CurPage + 1) + "/" + Page;
        }

        private void NextPage()
        {
            CurPage = CurPage == Page - 1 ? Page - 1 : CurPage + 1;
            RefreshPage();
        }

        private void PrevPage()
        {
            CurPage = CurPage <= 1 ? 0 : CurPage - 1;
            RefreshPage();
        }

        private void OnStatusChange(Follower obj)
        {
            if (obj.IsFighting)
            {
                FollowerCollectLogic.GoRest(obj);
            }
            else
            {
                FollowerCollectLogic.GoFight(obj);
            }
        }

        private void OnFightStatusSwitch()
        {
            ShowDetail.RefreshInfo();
        }
    }
}

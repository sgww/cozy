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

        public FollowerListUiLayer()
        {
            var listlable = new CCLabel("佣兵列表", "微软雅黑", 14)
            {
                Position = new CCPoint(92, 20),
                Color = CCColor3B.Black
            };

            FollowerList    = PlayerObject.Instance.Self.AllFollower;
            Page            = (FollowerList.Followers.Count + 8) / 9;

            AddChild(listlable, 100);

            foreach(var obj in FollowerList.Followers)
            {
                var fs = new FollowerSprite(obj, true)
                {
                    Visible = false,
                };
                this.AddChild(fs);
                SpriteList.Add(fs);
            }

            AllFollower = new CCLabel("总数" + 20 + "/" + FollowerList.Followers.Count, "微软雅黑", 14)
            {
                Position    = new CCPoint(92, 47),
                Color       = CCColor3B.White,
            };
            AddChild(AllFollower, 100);

            PageNumber = new CCLabel((CurPage + 1) + "/" + Page, "微软雅黑", 14)
            {
                Position    = new CCPoint(389, 47),
                Color       = CCColor3B.White
            };
            AddChild(PageNumber, 100);
            LastPageButton = new CozySampleButton(473, 27, 78, 36)
            {
                Text        = "上一页",
                FontSize    = 14,
                OnClick     = () =>
                {
                    PrevPage();
                },
            };
            NextPageButton = new CozySampleButton(585, 27, 78, 36)
            {
                Text        = "下一页",
                FontSize    = 14,
                OnClick     = () => 
                {
                    NextPage();
                },
            };
            AddChild(NextPageButton, 100);
            AddChild(LastPageButton, 100);
            this.AddEventListener(LastPageButton.EventListener);
            this.AddEventListener(NextPageButton.EventListener);

            RefreshPage();
        }

        private void RefreshPage()
        {
            foreach(var obj in SpriteList)
            {
                obj.Visible = false;
                obj.Position = CCPoint.Zero;
            }

            int count = CurPage * 9;
            for (int i = 0; i < 3; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    if (count < FollowerList.Followers.Count)
                    {
                        SpriteList[count].Position  = new CCPoint(100 + j * (200 + 10), i * (100 + 10) + 100);
                        SpriteList[count].Visible   = true;
                    }
                    count++;
                }
            }
            PageNumber.Text = (CurPage + 1) + "/" + Page;
        }

        private void NextPage()
        {
            CurPage = CurPage == Page - 1 ? Page - 1 : CurPage + 1;
            RefreshPage(); 
        }

        private void PrevPage()
        {
            CurPage = CurPage == 0 ? 0 : CurPage - 1;
            RefreshPage();
        }
    }
}

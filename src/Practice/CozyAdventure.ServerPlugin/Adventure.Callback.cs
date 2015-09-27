﻿using CozyAdventure.Protocol.Msg;
using CozyNetworkHelper;
using CozyNetworkProtocol;
using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyAdventure.ServerPlugin
{
    public partial class AdventurePlugin
    {
        [CallBack(typeof(RegisterMessage))]
        public void OnRegisterMessage(NetBuffer buff, MessageBase msg)
        {
            var im = buff as NetIncomingMessage;

            if (im != null)
            {
                RegisterMessageImpl(im, msg);
            }
        }

        [CallBack(typeof(LoginMessage))]
        public void OnLoginMessage(NetBuffer buff, MessageBase msg)
        {
            var im = buff as NetIncomingMessage;

            if (im != null)
            {
                LoginMessageImpl(im, msg);
            }
        }

        [CallBack(typeof(PullMessage))]
        public void OnPullMessage(NetBuffer buff, MessageBase msg)
        {
            var im = buff as NetIncomingMessage;

            if (im != null)
            {
                PullMessageImpl(im, msg);
            }
        }

        [CallBack(typeof(GotoMapMessage))]
        public void OnGotoMapMessage(NetBuffer buff, MessageBase msg)
        {
            var im = buff as NetIncomingMessage;

            if (im != null)
            {
                GotoMapMessageImpl(im, msg);
            }
        }

        [CallBack(typeof(GotoHomeMessage))]
        public void OnGotoHomeMessage(NetBuffer buff, MessageBase msg)
        {
            var im = buff as NetIncomingMessage;

            if (im != null)
            {
                GotoHomeMessageImpl(im, msg);
            }
        }
    }
}

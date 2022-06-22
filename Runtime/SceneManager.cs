//---------------------------------------------------------------------//
//                    GNU GENERAL PUBLIC LICENSE                       //
//                       Version 2, June 1991                          //
//                                                                     //
// Copyright (C) Wells Hsu, wellshsu@outlook.com, All rights reserved. //
// Everyone is permitted to copy and distribute verbatim copies        //
// of this license document, but changing it is not allowed.           //
//                  SEE LICENSE.md FOR MORE DETAILS.                   //
//---------------------------------------------------------------------//
using EP.U3D.LIBRARY.BASE;
using System;

namespace EP.U3D.LIBRARY.SCENE
{
    public static class SceneManager
    {
        public delegate void SwapDelegate(Scene last, Scene current);
        public static Scene Last;
        public static Scene Current;
        public static Scene Next;
        public static event SwapDelegate OnSwap;

        public static void Update()
        {
            Current?.Update();
            if (Next != null)
            {
                Current?.Stop();
                Last = Current;
                Current = Next;
                Next = null;
                Current?.Start();
                OnSwap?.Invoke(Last, Current);
            }
        }

        public static void Goto(Scene scene)
        {
            if (!scene.Inited)
            {
                scene.Inited = true;
                scene.Awake();
            }
            if (Next != null && scene.Name() == Next.Name()) return;
            if ((Current != null && Current.Name() != scene.Name()) || Current == null)
            {
                Next = scene;
            }
            else
            {
                Helper.LogWarning(Constants.RELEASE_MODE ? null : "Goto: can not go to same scene.");
            }
        }
    }
}
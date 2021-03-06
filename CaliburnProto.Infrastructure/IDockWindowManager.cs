﻿// Credit: BladeWise
// http://caliburnmicro.codeplex.com/discussions/231809

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;

namespace CaliburnProto.Infrastructure
{
    public enum DockSide
    {
        None,
        Left,
        Top,
        Right,
        Bottom
    }

    public interface IDockWindowManager : IWindowManager
    {
        /// <summary>
        /// Shows a docked window.
        /// </summary>
        /// <param name = "viewModel">The view model.</param>
        /// <param name = "context">The context.</param>
        /// <param name="selectWhenShown">If set to <c>true</c> the window will be selected when shown.</param>
        /// <param name = "dockSide">The dock side (Left, Right, Top, Bottom).</param>
        void ShowDockedWindow(object viewModel,
                              object context = null,
                              bool selectWhenShown = true,
                              DockSide dockSide = DockSide.Left);

        /// <summary>
        /// Shows a floating window.
        /// </summary>
        /// <param name = "viewModel">The view model.</param>
        /// <param name = "context">The context.</param>
        /// <param name="selectWhenShown">If set to <c>true</c> the window will be selected when shown.</param>
        void ShowFloatingWindow(object viewModel,
                                object context = null,
                                bool selectWhenShown = true);

        /// <summary>
        /// Shows a document window.
        /// </summary>
        /// <param name = "viewModel">The view model.</param>
        /// <param name = "context">The context.</param>
        /// <param name="selectWhenShown">If set to <c>true</c> the window will be selected when shown.</param>
        void ShowDocumentWindow(object viewModel,
                                object context = null,
                                bool selectWhenShown = true);
    }
}

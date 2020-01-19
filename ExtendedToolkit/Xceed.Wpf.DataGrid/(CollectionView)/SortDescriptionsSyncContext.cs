﻿/************************************************************************

   Extended WPF Toolkit

   Copyright (C) 2010-2012 Xceed Software Inc.

   This program is provided to you under the terms of the Microsoft Public
   License (Ms-PL) as published at http://wpftoolkit.codeplex.com/license 

   For more features, controls, and fast professional support,
   pick up the Plus edition at http://xceed.com/wpf_toolkit

   Visit http://xceed.com and follow @datagrid on Twitter

  **********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace Xceed.Wpf.DataGrid
{
  internal class SortDescriptionsSyncContext
  {
    //public bool IsInitializing
    //{
    //  get
    //  {
    //    return m_isInitializing;
    //  }
    //  set
    //  {
    //    m_isInitializing = value;
    //  }
    //}

    public bool ProcessingSortSynchronization
    {
      get
      {
        return m_processingSortSynchronization;
      }
      set
      {
        m_processingSortSynchronization = value;
      }
    }

    //public bool SynchronizeSortDelayed
    //{
    //  get
    //  {
    //    return m_synchronizeSortDelayed;
    //  }
    //  set
    //  {
    //    m_synchronizeSortDelayed = value;
    //  }
    //}

    private bool m_processingSortSynchronization;
    //private bool m_synchronizeSortDelayed;
    //private bool m_isInitializing;

  }
}
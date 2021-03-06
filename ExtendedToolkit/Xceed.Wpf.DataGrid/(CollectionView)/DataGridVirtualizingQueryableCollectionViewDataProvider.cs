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
using System.Linq;
using System.Text;

namespace Xceed.Wpf.DataGrid
{
  internal class DataGridVirtualizingQueryableCollectionViewDataProvider : DataGridCollectionViewBaseDataProvider
  {
    #region CONSTRUCTORS

    public DataGridVirtualizingQueryableCollectionViewDataProvider( DataGridVirtualizingQueryableCollectionViewSource parentSource )
      : base( parentSource )
    {
    }

    #endregion CONSTRUCTORS

    #region INTERNAL METHODS

    internal override DataGridCollectionViewBase EnsureDataGridCollectionViewBaseCore()
    {
      DataGridVirtualizingQueryableCollectionViewSource parentSource = this.ParentSource as DataGridVirtualizingQueryableCollectionViewSource;

      if( !parentSource.IsInitialized )
        return null;

      return new DataGridVirtualizingQueryableCollectionView( 
        parentSource.QueryableSource, 
        parentSource.AutoCreateItemProperties,
        parentSource.PageSize,
        parentSource.MaxRealizedItemCount );
    }

    #endregion INTERNAL METHODS
  }

}

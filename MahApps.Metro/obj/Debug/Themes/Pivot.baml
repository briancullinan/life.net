   M S B A M L   `   `   `  ����H  DMahApps.Metro, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:MahApps.Metro.ControlsMahApps.Metro.Controls  5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   PivotHeaderTemplate  HeaderFontSize  HeaderFontFamily  PivotListViewItem  �� 
TargetType �� Property  
BlackBrush �� Value 	 Gray3 �� 
TargetType ��Name J� SnapsToDevicePixels ��Name J� Opacity �� Triggers	 P� Value
 P� Property �� 
TargetName"    MahApps.Metro.Controls.Pivot �Name � RowDefinitions    HeaderTemplate �� RelativeSource �� AncestorType ��Name �� Orientation  SubHeaderFontSize ��Name ��Name7   1MahApps.Metro.Controls.ScrollViewerOffsetMediator  Name   ScrollViewer �� ElementName � 	Resources  Key �� TargetProperty �� 
TargetName9  3System.Windows.Media.Animation.EasingDoubleKeyFrame O� KeyTime O� Value   EasingFunction/  )System.Windows.Media.Animation.CircleEase&    MahApps.Metro.Controls.PivotItem�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      1Controls$clr-namespace:MahApps.Metro.Controls  5      %
  &	        &	 S     '   �    7   7  '  c	    �� 5      .R�5   
   �� #���  6%   #���  6Q   ��6   �� 6Q   5      �� 5
      "  ��6&   .�5   
   ��  � ��# �  6'   �� 5   
    � ��$ Stretch=�67   �� 5   
    � ��$ Center=�65   �� 5   
    � ��$ 25��6&   �� 5   
    � ��$ 0,0,0,0q�6#   �� 5   
    � ��# �  6/   �� 5   
    � �� 5      �� 5      " ��6"   .R�5      �� $ Bd��6    . ._�5      � .+�5      �� #��z��63   �� 5      $ contentPresenter��6/   $y�	Collapsed=�6I   $
 0.35Q�6`   5      5       5      P� 5      	 6G   Z� 
 �@��ColumnCollection. �5      �� $ contentPresenter��6%    � y�$ Visible=�6Y   5      P� 5      
 �@��
IsSelected	 . . �5      �� $ contentPresenter��6%    �@J�Opacity$ 1Q�6V   5       P� 5!      
 �@J�IsMouseOver	 . . �5"      �� $ contentPresenter��6%    �@J�Opacity$ 1Q�6V   5#      5%      5'      5(      �� 5+      "    6   .�5,   
   ��  �@  HeaderTemplate8   6+   �� 5-   
    � �� 5.      �� 5/      "   6"   .R�50      � $
 root��6    51      �� 52      $
��Auto �6,   �� 53      $
��Auto �6,   �� 54      $��* �6,   .+�56      �� $��0��6+   #��z 6�   ��68   �� *Header+ �� "   6�   �� 57      $ PART_Headers��58      $��1��59      8�� $��Disabled=�5=      $��Disabled=�5>      ��5:      Z� ��5;      Z� ��5?      �� *Items+ �� "   ��5@      �� 5A   "   .R�5B   &   �� $ 
Horizontal=�61   5C   #   ��5E      �� 5F   "   .R�5G   &   �� #���  6H   #���  6w   $��0,0,25,0q�6�   ��60   �� *Header+6�   5H   #   5J      �� 5L      $ PART_Scroll��64   $��2��6'   $��Disabled=�6I   $
��Auto=�6p   $��Stretch=�6�   .��5M      �� $ itemscontrol��6+   $��	0,0,250,0q�6A   ��6T   �� *Items+ �� "   ��5N   "   �� 5O   &   .R�5P   *   �� $ 
Horizontal=�65   5Q   '   5S      5T        5V      $ PART_Mediator��6>    6U   �� $ PART_Scroll�� 5W      ���� 5X   "   Storyboard1 .��5Y   &   U� $ HorizontalOffset�6D   $ PART_Mediator��6q   .\�5Z   *     edkf��$ 0:0:0.6��6M   $ 0Q�6_    5[   .     5\   2   5^   +   5_   '   5`   #   5b      5c      5d      5e      5g      5h      �� 5j      "   6   .�5k   
   ��  � �� 5l      �� 5m      "  6"   .R�5n      �� 5o      5q      5r      5s      
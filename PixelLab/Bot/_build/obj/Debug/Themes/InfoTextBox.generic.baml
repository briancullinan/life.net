   M S B A M L   `   `   `  ����G  CPixelLab.Wpf, Version=6.0.1.0, Culture=neutral, PublicKeyToken=null+clr-namespace:PixelLab.WpfPixelLab.Wpf  5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089    PixelLab.Wpf.InfoTextBox  �� 
TargetType �� BasedOn �� Property �� Value �� 
TargetType � 	Resources   Key g� Duration	 W� To �� TargetProperty	 �� 
TargetName
 W� From!   PixelLab.Wpf.TemplateBorder ��Name ��Name    TextBoxInfo J� IsHitTestVisible J� Opacity �� Triggers ]� 
Conditions �� Property �� Value N� EnterActions  enterGotFocus N� ExitActions  exitGotFocus P� Property P� Value  enterHasText  exitHasText �� 
TargetName�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      "libclr-namespace:PixelLab.Wpf  5   	   %z  '         7��7  7  7  7  �� 5      "    6   8   .�5   
   ��  � ��$ 1q�6#   �� 5   
    � �� 5      �� 5	      "   6"    5
      ���� 5      enterGotFocus  .��5      W� $ 0:0:0.2D�6.   $	 .33X�6A   $ Opacity�5      $	 Message��6+   5      �� 5      exitGotFocus  .��5      W� $ 0:0:0.4D�6.   $ Opacity�5      $	 Message��6+   5      �� 5      enterHasText  .��5      W� $ 0:0:0.2D�6.   $	
 .33X�6A   $ 0X�6L   $ Opacity�5      $	 Message��6+   5      �� 5      exitHasText  .��5      W� $ 0:0:0.4D�6.   $ Opacity�5      $	 Message��6+   5      5      .R�5        $��Bd��6)   ._�5       � .+�5!      �� $ PART_ContentHost��6+   $��Center=�6E   $��3q�6`   �� 5"      $ Message��6M   #��z 6(   
����  � .  $��Italic$�5$   	   $
 0.67Q�6   $
��Left=�6+   $��Center=�6F   $��6,0,0,0q�5%   	   5&      5'       5(      ]� 5*       5+      �� 5,   "    �@  HasText .  6?   �� 5-   "    �@J�	IsFocused . 6A    50      �� 51   "   8�� 62    53      �� 54   "   8�� 62   56      P� 58       �@  HasText .  59      �� 5:   "   8�� 62    5<      �� 5=   "   8�� 62   5?      P� 5A       � {� .  . �5C      �� $ Bd��6%   # � ��5E   
    � ���� 5F      # � ��5G   
    � ��5H      P� 5K       � ��$
 Auto��63   . �5L      ��  � ��$	 100��69   5M      P� 5P       � ��$
 Auto��64   . �5Q      ��  � ��$ 20��6:   5R      5U      5W      5X      5Z      
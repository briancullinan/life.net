   M S B A M L   `   `   `  ����G  CPixelLab.Wpf, Version=6.0.1.0, Culture=neutral, PublicKeyToken=null+clr-namespace:PixelLab.WpfPixelLab.Wpf  5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089  �� 
TargetType �� BasedOn   pageButtonStyle �� Property �� Value  pageRepeatButtonStyle �� DataType!    PixelLab.Wpf.ZapCommandItem ! DefaultZapCommandItemTemplate �� Path ' #ZapScrollerCommandItemsControlStyle �� 
TargetType �� Orientation K� IsItemsHost   PixelLab.Wpf.ZapScroller	 �� RelativeSource
   CommandItemTemplate   PixelLab.Wpf.ZapDecorator   PixelLab.Wpf.ZapPanel�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      "libclr-namespace:PixelLab.Wpf  5      %�  &	        7��&	 q     7��&	     7   &	 p    '  6    7  7   7  7  7  7   �� 5      "  ��6   8   .�5   
   ��  � ��$ 30��6"   �� 5   
    � ��$ 30��6#   5	      �� 5      "  ��6   8   .�5   
   ��  � ��$ 30��6"   �� 5   
    � ��$ 30��6#   �� 5   
    �@��Interval$	 500��6%   5      �� 5      "   6   .R�5   
   �� 8��  ��6   �� ��6N   �� $ Number�5      �� 5      "  ��6   .�5   
   ��  � |� .  6&   �� 5   
    � �� 5      �� 5      " ��6"   .R�5      �� $ 
Horizontal=�6!    . 6:   5      5      5      �� 5       "   6   .�5!   
   ��  �@ CommandItemTemplate8   60   �� 5"   
    � �� 5#      �� 5$      "  6"   .R�5%      ]� .+�5'      �� $ 
Horizontal=�6%   $	��Top=�6>   $��Center=�6S   .+�5(      �� $��<<��6%   8�� ��68   �� 	 �� *TemplatedParent+$ FirstCommand�5)      �� 5*      $��<��6+   8�� ��6:   �� 	 �� *TemplatedParent+$ PreviousCommand�5+      �� 5,      8�� #��z
 5.      ��5,   +   �� 	 �� *TemplatedParent+$ Commands�5.      �� 5/      $��>��6+   8�� ��6:   �� 	 �� *TemplatedParent+$ NextCommand�50      �� 51      $��>>��6%   8�� ��68   �� 	 �� *TemplatedParent+$ LastCommand�52      53        55      $��PART_ZapDecorator��6+   ._�56         . 6+   57      59      5:      5<      5=      5?      
   M S B A M L   `   `   `  ����H  DMahApps.Metro, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:MahApps.Metro.ControlsMahApps.Metro.Controls  5      =#clr-namespace:MahApps.Metro.ActionsMahApps.Metro.Actions  5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35  �� MergedDictionaries �� Source   HeaderTemplate ]�Name ��Name  FlyoutWhiteBrush  MetroCircleButtonStyle.   (System.Windows.Interactivity.Interaction    Triggers/  )System.Windows.Interactivity.EventTrigger   	EventName.  (System.Windows.Interactivity.TriggerBase   Actions/   )MahApps.Metro.Actions.SetFlyoutOpenAction   Value8  2System.Windows.Interactivity.TargetedTriggerAction   TargetObject	 �� RelativeSource
 �� AncestorType#   MahApps.Metro.Controls.Flyout j� Stretch !� Visual J� Clip � UseLayoutRounding �� Left
 �� Top �� Stretch ��Name �� Triggers �� Value �� Binding �� 
TargetName �� Property �� Value �� ScaleX �� Angle  FlyoutTemplate �� 
TargetType �Name'  !System.Windows.VisualStateManager   VisualStateGroups%  System.Windows.VisualStateGroup   States   System.Windows.VisualState   
Storyboard  g� 	BeginTime! �� TargetProperty" �� 
TargetName9	  3System.Windows.Media.Animation.EasingDoubleKeyFrame# O� KeyTime$ O� Value% 	  EasingFunction.
  (System.Windows.Media.Animation.CubicEase7  1System.Windows.Media.Animation.EasingFunctionBase&   
EasingMode' U�Name( ��Name)   HeaderTemplate* �� Triggers+ �� 
TargetType, �� 	Resources  Key  FlyoutWhiteColor- �� Color-  'System.Windows.Input.KeyboardNavigation�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      1Controls$clr-namespace:MahApps.Metro.Controls  5      Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      /Actions#clr-namespace:MahApps.Metro.Actions  5        5      �� 5   
   $H Bpack://application:,,,/MahApps.Metro;component/Styles/Colours.xaml?�6   %V  &	       &	 z    '  k    7  7   7  �� 5      .R�5   
   ]� $ dpHeader��6   $��10q�5      $	��Top=�5      ��. .+�5      �� $	 nav��6   $
��Left=�5      #���  5      #���  5      $��40��5      $��40��5      $��Segoe UI Symbol)�5      $��16(�5      $	��Top=�5       5        5      $ Click��6%   . 5         .   66   �� 	 �� "
  5   6   5      .��5       �� $��20��6   $��15��5!      ��5"      !� 5#      $
 Fill=�6&    5$      �� 5%   "   $��48��6)   $��48��5&   )   4 ��        � l�    � l� l��     l��         .  .+�5)   &   F� $��25��6+   $��18��5*   +   $ 12��5+   +   $ 15��5,   +   $
 Fill=�5-   +   #���  5.   +   a���� � ��O� � ���ѐ�����@o@@������@���@�4 6n������� �@o@��� ѐ���O� � ��O5/   +   50   #   52      54      55      �� 56      $ PART_BackButton��57      #���  58      $��20(�59      $��15,0,0,0q�5:      $��Center=�5;      ��56      �� 5;      5<       5=   
   �� 5>      $
 Left��5?       5>      �� *
Position+	 �� "
  .a�5@      �� $	 nav��6    � ��$ Right=�5B      �� 5C      $ PART_BackButton��6    �@��TextAlignment$ Right=�5E      �� 5F      $ PART_BackButton��6    � ��$ 0,0,15,0q�5H      �� 5I      $	 nav��6    �@�LayoutTransform 5K      �� 5L      $ -1Q�6)   5N      5O      �� 5P      $	 Top��5Q       5P      �� *
Position+	 �� "
  .a�5R      �� $	 nav��6    �@�LayoutTransform 5T      �� 5U      $	 -90Q�6*   5W      �� 5X      $ dpHeader��6    � ��$ Bottom=�6L   5Y      �� 5Z      $ Bottom��5[       5Z      �� *
Position+	 �� "
  .a�5\      �� $	 nav��6    �@�LayoutTransform 5^      �� 5_      $ 90Q�6*   5a      5b      5d      �� 5f      "  5g      .R�5h   
   � $
 root��6   #��z��5i      z�5j      T� 5k       5m        5n      . 5o        Default��6"     5p      Hide��. 5q      �� .��5r      U� $  00:00:00X�6<   $8! 2(UIElement.RenderTransform).(TranslateTransform.X)�5s   <   $
" root��5t   <   .\�5u   "   	  $# 00:00:00.500��67   $$ 0Q�5v   7   	hideFrame��% 5x   &   
  5y   *   $& EaseOut=�64   5{   #   5|      U� 5}      $  00:00:00X�6<   $8! 2(UIElement.RenderTransform).(TranslateTransform.Y)�5~   <   $
" root��5   <   .\�5�   "   	  $# 00:00:00.500��67   $$ 0Q�5�   7   
hideFrameY��% 5�   &   
  5�   *   $& EaseOut=�64   5�   #   5�      5�      5�        5�      Show��. 5�      �� .��5�      U� $' trabslateShow��5�   <   $  00:00:00X�5�   <   $8! 2(UIElement.RenderTransform).(TranslateTransform.X)�5�   <   $
" root��5�   <   .\�5�   "   	  $# 00:00:00.600��67   $$ 0Q�5�   7   	showFrame��% 5�   &   
  5�   *   $& EaseOut=�64   5�   #   5�      U� 5�      $  00:00:00X�6<   $8! 2(UIElement.RenderTransform).(TranslateTransform.Y)�5�   <   $
" root��5�   <   .\�5�   "   	  $# 00:00:00.600��67   $$ 0Q�5�   7   
showFrameY��% 5�   &   
  5�   *   $& EaseOut=�64   5�   #   5�      5�      5�      5�      .+�5�      ]� .+�5�      �� $( PART_Header��6#   $	��Top=�5�   #   $��Header��5�   #   #��z) 5�   #   �� 5�      $( PART_Content��6#   $��Bottom=�5�   #   5�      5�      5�      * 5�   
   �� 5�      $	 Top��5�       5�      �� *
Position+	 �� *Self+.a�5�      �� $ PART_Header��6    � ��$
 Left=�5�      �� 5�      $ PART_Content��6    � ��$ Right=�5�      5�      �� 5�      $ Bottom��5�       5�      �� *
Position+	 �� *Self+.a�5�      �� $ PART_Header��6    � ��$
 Left=�5�      �� 5�      $ PART_Content��6    � ��$ Right=�5�      5�      5�      �� 5�      "+  6   , 5�   
   ���� 5�      LabelTextBrush 8-   5�      5�      .�5�   
   ��  �@ TabNavigation$ Cycle=�5�      �� 5�   
    � ��$ Stretch=�5�      �� 5�   
    � ��$ Stretch=�5�      �� 5�   
    �@ HeaderTemplate8  5�      �� 5�   
    � �� �	#FF2525255�      �� 5�   
    � ��8  5�      �� 5�   
    �@�FocusVisualStyle 5�      Z� 5�      5�      
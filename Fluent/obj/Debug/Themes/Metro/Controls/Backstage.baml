   M S B A M L   `   `   `  ����M  IFluent, Version=2.1.0.0, Culture=neutral, PublicKeyToken=3e436e32a8c5546fclr-namespace:FluentFluent  5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089 (  $BackstageToggleButtonControlTemplate  �� 
TargetType    Fluent.Backstage ��Name ��Name ��Name �� Triggers ]� 
Conditions �� Property �� Value �� 
TargetName	 �� Property
 �� Value P� Property P� Value   Fluent.GrayscaleEffect �� 
SourceName  RibbonBackstageStyle �� 
TargetType �� RelativeSource �� AncestorType   Fluent.MetroWindow  DefaultFontFamily  DefaultFontSize   Fluent.FrameworkHelper �� BasedOn�� 5      F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     5      Fluentclr-namespace:Fluent  5      8d2http://schemas.microsoft.com/expression/blend/2008  69   Bmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5      %�  &	        &	     7   '   �    7  �� 5      "    6C   .R�5   
   �� $ border2��6   $��60��6"   $��24��60   #��z��6<   $��0,0,1,0q�6f   ._�5      � .+�5	      �� $ 	rectangle��6   �� 5
      $ contentPresenter��6#   $��Header��6=   $��	10,0,10,0q�6T   $��Center=�5   #   $��Center=�6>   5      5       5   
   ]� 5       5      �� 5       �@J�IsMouseOver . 67   ./�5      �� $ 	rectangle��6)   	 � ��
 �	#3FFFFFFF6@   5      P� 5       � {� .  . �5      �� $ border2��6,   	 �@J�Opacity$	
 0.5Q�6A   �� 5      $ contentPresenter��6+   	 �@J�Effect
 5        5      5      5      P� 5       �@  IsOpen . . �5      �� $ border2��6+   	 � ��$
 0q�6@   �� 5      $ 	rectangle��6/   	 � y�$
 	Collapsed=�6F   5       ]� 5!       5"      �� 5#      $ border2��6D    �@J�IsMouseOver . �� 5$       �@  IsOpen . 62   ./�5&      �� $ 	rectangle��6/   	 � y�$
 	Collapsed=�6F   5'      5)      �� 5+      "   5,      .�5-   
   �� 	 � ��8
   6%   �� 5.   
   	 � ��
 5/      �� *RibbonThemeColor+ �� *FindAncestor+"  �� 50   
   	 � ��#
 �  6'   �� 51   
   	 � ��#
 �  6%   �� 52   
   	 � ��

 �����6'   �� 53   
   	 �@J�SnapsToDevicePixels
 . 60   �� 54   
   	 �@ UseLayoutRounding
 . 6E   55      �� 57      "   6   8   63   58      
   M S B A M L   `   `   `  ����T  PWPFExtensions, Version=1.1.0.0, Culture=neutral, PublicKeyToken=39de8b1cac8f3dd4jEclr-namespace:WPFExtensions.AttachedBehaviours;assembly=WPFExtensions WPFExtensions.AttachedBehaviours  5      U QGraphSharp.Controls, Version=1.0.4880.25134, Culture=neutral, PublicKeyToken=null=#clr-namespace:GraphSharp.ConvertersGraphSharp.Converters 5      9!clr-namespace:GraphSharp.ControlsGraphSharp.Controls 5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0894   .GraphSharp.Converters.EdgeRouteToPathConverter   routeToPathConverter.  (GraphSharp.Converters.PointArrayToString  pointsToStringConverter%  GraphSharp.Controls.EdgeControl  �� 
TargetType �� Property �� Value �� 
TargetType F�Name   StrokeThickness � ToolTip `� 	Converter �� Path	 �� RelativeSource/  )GraphSharp.Controls.GraphElementBehaviour
 �� Triggers P� Property P� Value ]� 
Conditions �� Property �� Value'  !GraphSharp.Controls.VertexControl   Vertex4   .WPFExtensions.AttachedBehaviours.DragBehaviour �� Mode/  )GraphSharp.Controls.CompoundVertexControl � 	Resources 9� 
IsExpanded ��Name   Vertices �� Orientation �� 
StartPoint �� EndPoint � Color � Offset%  GraphSharp.Controls.GraphCanvas�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      VextbehaviourEclr-namespace:WPFExtensions.AttachedBehaviours;assembly=WPFExtensions  5      2
Converters#clr-namespace:GraphSharp.Converters  5      .Controls!clr-namespace:GraphSharp.Controls  5      %�  &	        &	      '  &     7   '  z    '         5      6*     5      6$   �� 5
      "   6   .�5   
   ��  � �� 5      �� 5      "  6"   .R�5      F� $ edgePath��5      #��z��5      #��z 5      $��1��5      $��1��5      # z 5      ��5      B� 5      ��5   &   `� 8   .1�5   *   �� $% Source.(Controls:GraphCanvas.X)�5   2   	 5   2   �� *TemplatedParent+5   2   �� 5   *   $% Source.(Controls:GraphCanvas.Y)�5   2   	 5   2   �� *TemplatedParent+5   2   �� 5   *   $ Source.ActualWidth�5   2   	 5   2   �� *TemplatedParent+5   2   �� 5   *   $ Source.ActualHeight�5   2   	 5   2   �� *TemplatedParent+5   2   �� 5    *   $% Target.(Controls:GraphCanvas.X)�5!   2   	 5    2   �� *TemplatedParent+5!   2   �� 5"   *   $% Target.(Controls:GraphCanvas.Y)�5#   2   	 5"   2   �� *TemplatedParent+5#   2   �� 5$   *   $ Target.ActualWidth�5%   2   	 5$   2   �� *TemplatedParent+5%   2   �� 5&   *   $ Target.ActualHeight�5'   2   	 5&   2   �� *TemplatedParent+5'   2   �� 5(   *   $ RoutePoints�5)   2   	 5(   2   �� *TemplatedParent+5)   2   5*   '   5,      5.      5/      51      �� 52   
    �@ HighlightTrigger 53      �� 	 �� *Self+$ IsMouseOver��� 54   
    � ��$ 1��55      �� 56   
    � ��$ 1��57      �� 58   
    � ��
 �  ��59      �� 5:   
    � ��
 �����5;      �� 5<   
    �@J�Opacity$	 0.5Q�5=      
 5>   
   P� 5?       �@ IsHighlighted . . �5A      ��  � ��
 �   �5B      5C      P� 5D       �@ IsSemiHighlighted . . �5F      ��  � ��
 � ���5G      5H      ]� 5I       5J      �� 5K       �@ IsSemiHighlighted . 5L       �� 5M       �@ SemiHighlightInfo$ InEdge��5N       ./�5P      ��  � ��
 �  ��5Q      5R      ]� 5S       5T      �� 5U       �@ IsSemiHighlighted . 5V       �� 5W       �@ SemiHighlightInfo$ OutEdge��5X       ./�5Z      ��  � ��
 ��  �5[      5\      5^      �� 5`      "   6   .�5a   
   ��  � �� 5b      �� 5c      "  6"   .R�5d      �� #��z��6   #��z��5e      #��z��5f      # z 5g      ._�5h      �� #��z 6+   5i      5j      5l      �� 5m   
    �@ HighlightTrigger 5n      �� 	 �� *Self+$ IsMouseOver��� 5o   
    �@ IsDragEnabled . 5p      �� 5q   
   	 �@ X 5r      �� 	 �� *Self+$ (Controls:GraphCanvas.X)�$ TwoWay=��� 5s   
   	 �@ Y 5t      �� 	 �� *Self+$ (Controls:GraphCanvas.Y)�$ TwoWay=��� 5u   
    � ��$ 2q�5v      �� 5w   
    � ��
 ���� 5x      
 5y   
   P� 5z       �@ IsHighlighted . . �5|      ��  � ��
 � ���5}      �� 5~       � ��$ 2q�5      5�      P� 5�       �@ IsSemiHighlighted . . �5�      ��  � ��
 � ���5�      �� 5�       � ��$ 2q�5�      5�      ]� 5�       5�      �� 5�       �@ IsSemiHighlighted . 5�       �� 5�       �@ SemiHighlightInfo$ Source��5�       ./�5�      ��  � ��
 �  ��5�      �� 5�       � ��$ 2q�5�      5�      ]� 5�       5�      �� 5�       �@ IsSemiHighlighted . 5�       �� 5�       �@ SemiHighlightInfo$ Target��5�       ./�5�      ��  � ��
 ��  �5�      �� 5�       � ��$ 2q�5�      5�      5�      �� 5�      "   6   .�5�   
   ��  � �� 5�      �� 5�      "  6"    5�      ���� 5�      '        "   6    .�5�      ��  � ��
 �����5�   %   5�      5�      .R�5�      �� #��z��6   #��z��5�      #��z��5�      ._�5�      9�  6#   �� 	 �� *TemplatedParent+$ 
IsExpanded�$ TwoWay=���5�      �� 5�   "   #��z 63   # z 6V   .��5�      �� $ PART_InnerCanvas��6+   #��z 5�   +   ��5�   "   �� 5�   &   .R�5�   *   �� $ Vertical=�65   5�   '   5�      5�      5�      5�      5�      �� 5�   
    �@ HighlightTrigger 5�      �� 	 �� *Self+$ IsMouseOver��� 5�   
    �@ IsDragEnabled . 5�      �� 5�   
   	 �@ X 5�      �� 	 �� *Self+$ (Controls:GraphCanvas.X)�$ TwoWay=��� 5�   
   	 �@ Y 5�      �� 	 �� *Self+$ (Controls:GraphCanvas.Y)�$ TwoWay=��� 5�   
    � ��$ 2q�5�      �� 5�   
    � ��
 �����5�      �� 5�   
    � �� 5�      �� 5�      $	 0,0&�6&   $	 1,1&�5�   &   .��5�      � $ White��6#   $ 0Q�5�   #   � 5�      $ 	BurlyWood��6#   $ 1Q�5�   #   5�      5�      
 5�   
   P� 5�       �@ 
LayoutMode$ 	Automatic=�5�      . �5�      ��  � �� 5�      �� 5�      "  6*   .R�5�      �� #��z��6%   #��z��5�   %   #��z��5�   %   ._�5�   "   9�  6+   �� 	 �� *TemplatedParent+$ 
IsExpanded�$ TwoWay=���5�   &   �� 5�   *   #��z 6;   .��5�   &   �� $ PART_InnerCanvas��63   #��z 5�   3   ��5�   *   �� 5�   .   .R�5�   2     
�������6G   5�   /   5�   '   5�   #   5�      5�      5�      �� 5�       � �� 5�      �� 5�      $	 0,0&�6.   $	 1,1&�5�   .   .��5�      � $ White��6+   $ 0Q�5�   +   � 5�      $ Silver��6+   $ 1Q�5�   +   5�      5�      5      P� 5      �@ IsHighlighted . . �5     ��  � ��
 � ���5     �� 5      � ��$ 2q�5     5     P� 5      �@ IsSemiHighlighted . . �5
     ��  � ��
 � ���5     �� 5      � ��$ 2q�5     5     ]� 5      5     �� 5      �@ IsSemiHighlighted . 5      �� 5      �@ SemiHighlightInfo$ Source��5      ./�5     ��  � ��
 �  ��5     �� 5      � ��$ 2q�5     5     ]� 5      5     �� 5      �@ IsSemiHighlighted . 5      �� 5      �@ SemiHighlightInfo$ Target��5       ./�5"     ��  � ��
 ��  �5#     �� 5$      � ��$ 2q�5%     5&     5(     5)     
   M S B A M L   `   `   `  ���� B  >AduSkin, Version=1.1.1.8, Culture=neutral, PublicKeyToken=nullP5clr-namespace:AduSkin.Controls.Metro;assembly=AduSkinAduSkin.Controls.Metro  
 ED.GUI   ED.GUI.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     Das5clr-namespace:AduSkin.Controls.Metro;assembly=AduSkin     -   $	��win��$��190.91��$��384.711��  � WindowStartupLocation$  CenterScreen=�$	��200��$	��390��(   "AduSkin.Controls.Metro.MetroWindow   BorderBrush �#16a085 � Title �� *Title+ � Icon Z�  � 	Resources ��%       Menu +   %AduSkin.Controls.Metro.MetroTitleMenu  $
��auto��.@�/   )AduSkin.Controls.Metro.MetroTitleMenuItem  $��菜单��.@�(   "AduSkin.Controls.Metro.AduCheckBox  -   $��连接数据库�� d� 	IsChecked �� *JoinDb+*   $AduSkin.Controls.Metro.MetroMenuItem  -   $��设置��$y�	Collapsed=�  -   $��退出��$y�	Collapsed=�.���   �� 
TargetType �� Property	 �� Value
 �� 
TargetType �� CornerRadius �� 	ClickMode �� Mode �� RelativeSource J� SnapsToDevicePixels ��Name �� Triggers P� Property P� Value �� Triggers J� IsHitTestVisible �� SelectionBoxItem �� SelectionBoxItemTemplate ��Name �� 
IsReadOnly � HorizontalOffset � ActualWidth �� IsDropDownOpen �� Color K� IsItemsHost-  'System.Windows.Input.KeyboardNavigation   DirectionalNavigation  � RowDefinitions'  !System.Windows.VisualStateManager!   VisualStateGroups%  System.Windows.VisualStateGroup"   States 	  System.Windows.VisualState# 	  
Storyboard$ g� RepeatBehavior% �� 
TargetName& �� TargetProperty8
  2System.Windows.Media.Animation.EasingPointKeyFrame' $� KeyTime( $� Value) J� Opacity* �Name+ � ColumnDefinitions, ��Name- J� RenderTransformOrigin. �� ScaleY/ �� ScaleX0 �� AngleY1 �� AngleX2 �� Angle3 (� StretchDirection4 �� Path5 �� StringFormat6 �� 
TargetName��%   '��       '� {    '�� �	    �� " ��.���  � ��$		 0,5q���  � ��$		 120����  � ��$	 Stretch=���  � ��	 �#16a085��  � ��	 �� "
 ��.R��� 
�������$��1q�$ 	10,0,0,10��
������ ._�� .+�d� $ Press=�|�.  $��2q�$��0��$��0��$
��Auto�� �� *IsDropDownOpen+$ TwoWay=� �� *TemplatedParent+���� " d�.���  � ��$	 0����  � ��$	 0����  � ��$
	 Auto����  � ��$
	 Auto����  � ��
	 ���� ��  � ��	 �	#00000000��  � ��$	 2q���  � ��	 �� "
 d�.R�]� #��z����.   . .+��� $ Border��#��z��#��z��$��Right=�._�F� "����        ��g5  	= ���j     	#��z��$��Center=�$��Center=� P�  �@d�	IsChecked$
 TrueY� P�  � {� .  . ���  � ��#	 � ����  .  #��z #��z #��z��$��Center=�$��3q�$��Center=��� $ PART_EditableTextBox��$��Stretch=�|�. $y�	Collapsed=� .  � $ -1��#��z #��z |�.  $��Slide=�.���  . $��Stretch=�.+��� $��1,1,1,1q�
�������$��Stretch=�$ 5������ $ White����  . $��Stretch=�.����  . $ 	Contained=�$��Stretch=��� " �.���  �@�FocusVisualStyle	 Z� ��  �@J�SnapsToDevicePixels	 . ��  � ��$	 20����  � ��	 �#6fae5f��  � ��$	 10(���  � ��$		 5,0q���  � ��	 �� "
 �.R�� ���	#00000000  �� $
��Auto �!   CommonStates��." 	  Determinate��	  Indeterminate��.# �� $$ Forever��.��*� $% 	Animation��$'& !(UIElement.RenderTransformOrigin)�.'�
  $' 0:0:0��$( 0.5,0.5&�
  $' 0:0:1.5��$( 1.95,0.5&�
  $' 0:0:3��$( 0.5,0.5&�.+�� #��z��.+��� ���#000000$	 7.5��$
) 0.05Q��� ���#000000$��1q�$	 7.5��$	) 0.1Q�� #��z��.+��� $ 
PART_Track��� $* PART_Indicator��}�. $
��Left=�+ �� $, width1���� $, width2��$��0 �.+�� $* 	Animation��$- 0.5,0.5&�z�V� .~��� $. -1Q�$/ 1Q��� $0 0Q�$1 0Q��� $	2 180Q�T� .+��� #��z��$	 7.5��._�(� $
��Left=�$3 DownOnly=�#��z�� . .���� ���#ffffff . #��z��$��Center=�$- 0.5,0.5&�����  �� *TemplatedParent+$4 Value�$
5 {0}%��z�V� .~��� $. 1Q�$/ -1Q��� $0 0Q�$1 0Q��� $2 0Q�T� �� ���#000000$��1q�$	 7.5��$	) 0.1Q� P�  � {� .  . ���  � ��	 �#c5c5c5P�  �@�IsIndeterminate . . ��� $6 width1�� � ��$	 0.25* ��� $6 width2�� � ��$	 0.725* ��� " ��.���  � ��$	 50����  � ��$		 0,5q���  � ��	 �#16a085��  � ��	 �� "
 ��.R�� .+��� $��1��#��z��$��0q�$ 	0,10,10,0��#��z��._��� #��z��$��Center=�$��Center=� P�  �@J�IsMouseOver . . ���  � ��	 �#1abc9c  �� $
��auto ��� $��* ��� $
��auto �.+�� $��1��$��Center=�$��Center=�.+��� -   $��cbType��$	��100������ *Items+7 �� SelectedItem7 �� *SelectedItem+�� -   $��执行��]� -   8 ]�Name$8 dp��$��2��$��2��.+�� -   $	��bar��$��Bottom=�9 � Maximum$	9 100Q�y��� *
Progress+�� -	   $	��lab������ *	LabText+y��� *Lab+
   M S B A M L   `   `   `  ����P  LSENG403_AlarmClock_V2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:SENG403_AlarmClock_V2SENG403_AlarmClock_V2  5   	   *    $SENG403_AlarmClock_V2.NewAlarmWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:SENG403_AlarmClock_V2     5   	   X TXceed.Wpf.Toolkit, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	     � WindowStyle$
  None=�5   	   ���#efefef � Title$ NewAlarmWindow��5   	   $	��300��6     � SizeToContent$ Height=�6-   .��5      � -   5        .+�5   
   �� 
������ $��0��6'   $
��Left=�6<   $	��332��6W   
���   �$	��Top=�6s   $	��292��6�   �� 5   
   -   5         ��Name$ Title_Label��5      
���   �$��
Edit Alarm��68   $��Center=�6M   $��0,20,0,0q�6j   $	��Top=�6|   $��18(�6�   $
��Bold"�6�   � 5   
   -   5         �Name$ 	RadioGrid��5      $��26,153,10,284q�6"   .+�5      �� -   5         ��Name$ CheckBox_Sun��5      
���   �$��Sunday��6@   $
��Left=�6Q   $	��Top=�6l   �� 5      -   5        $ CheckBox_Mon��5      
���   �$��Monday��6@   $
��Left=�6Q   $	��Top=�6l   $��0,20,0,0q�6�   �� 5      -   5        $ CheckBox_Tue��5      
���   �$��Tuesday��6@   $
��Left=�6R   $	��Top=�6m   $��0,40,0,0q�6�   �� 5      -   5        $ CheckBox_Wed��5      
���   �$��	Wednesday��6@   $
��Left=�6T   $	��Top=�6o   $��80,0,0,0q�6�   �� 5      -   5        $ CheckBox_Thu��5      
���   �$��Thursday��6@   $
��Left=�6S   $	��Top=�6n   $��	80,20,0,0q�6�   �� 5      -	   5        $ CheckBox_Fri��5      
���   �$��Friday��6@   $
��Left=�6Q   $	��Top=�6l   $��	80,40,0,0q�6�   �� 5      -
   5        $ CheckBox_Sat��5      
���   �$��Saturday��6@   $
��Left=�6S   $	��Top=�6n   $��	180,0,0,0q�6�   �� 5      -   5        $ CheckBox_Daily��5      
���   �$��Daily��6B   $y�	Collapsed=�6R   $
��Left=�6i   $	��Top=�6�   $��
180,20,0,0q�6�   5      �� 5   
   -   5        $ repeat_checkBox��5      $��Repeat Alarm��6,   
���   �$
��Left=�6V   $��100,118,0,0q�6q   $	��Top=�6�   6�   � 5   
   -   5        $ 
OtherProps��5      $��26,251,21,34q�6#   .+�5      �� -   5         ��Name$
 Done��5      $
��Done��6#   $
��Left=�62   $��147,187,0,0q�6M   $	��Top=�6b   $��75��6z    J� RenderTransformOrigin$ 0.038,0.393&�6�   �� 5      -   5        $ Cancel��5      $��Cancel��6%   $
��Left=�66   $��
21,187,0,0q�6Q   $	��Top=�6e   $��75��6}   6�   �� 5      -   5         ��Name$ AlarmTone_comboBox��5      $
��Left=�63   $��
61,126,0,0q�6N   $	��Top=�6b   $	��120��6z   �� 5       -   5        	 ��Name$	 AlarmMessage��5       $
��Left=�6,   $��39��6G   $��	10,34,0,0q�6S   
 �� TextWrapping$

 Wrap=�6f   $��Set Alarm Label��6z   $	��Top=�6�   $	��225��6�   �� 5"      -   5        $ Title_Label_Copy��5"      
���   �$��Alarm Label��6A   $��73,0,0,0q�6W   $	��Top=�6i   $��14(�6�   $��91��6�   $
��Left=�6�   $
��Bold"�6�   �� 5#      -   5        $ Title_Label_Copy1��5#      
���   �$��Change Alarm Sound��6B   $��	45,97,0,0q�6_   $	��Top=�6r   $��14(�6�   $	��152��6�   $
��Left=�6�   $
��Bold"�6�   5%      "  Xceed.Wpf.Toolkit.TimePicker  5&   
   -   5          Name$ Alarm_TimePicker��5&      $
��Left=�64   $��	26,73,0,0q�6O   $	��Top=�6b   $	��245��6z   (  "System.Windows.Controls.DatePicker  5'   
   -   5          Name$ Alarm_DatePicker��5'      $
��Left=�60   $	��Top=�6K   $��
36,150,0,0q�6c   $	��225��6w   5)      5*      
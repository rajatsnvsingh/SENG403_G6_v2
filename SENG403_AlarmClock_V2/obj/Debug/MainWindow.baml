   M S B A M L   `   `   `  ����P  LSENG403_AlarmClock_V2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:SENG403_AlarmClock_V2SENG403_AlarmClock_V2  5   	   &     SENG403_AlarmClock_V2.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:SENG403_AlarmClock_V2     5   	   -   5          � 
ResizeMode$  CanMinimize=�5   	    � Title$ TDP Alarm Clock��5	   	   $	��720��6!   $
��1210��6.   
���   �.��5
      � .+�5   
   �� -   5         ��Name$ clockBackground��5      $2��,pack://application:,,,/Images/background.jpg��6*    �� BitmapScalingMode$
 Fant=�6`   $
��Left=�6�   $	��Top=�6�   $��	0,0,-56,0q�6�   $
��1278��6�   � 5   
   -   5         �Name$ TimeDisplay��5      $��334,237,292,253q�6$   .+�5      �� -   5         ��Name$ 	hourLabel��5      
�������$��04 : 20 ��6:   $
��Left=�6M   $	��Top=�6h   $	��128��6�   $	��368��6�   $	��100(�6�   $��Franklin Gothic Book)�6�   �� 5      -   5        $ minuteLabel��5      
�������$
��: 00��6<   $
��Left=�6K   $��
329,37,0,0q�6f   $	��Top=�6z   $��84��6�   $	��120��6�   $��Comic Sans MS)�6�   $��50(�6�   �� 5      -   5        $ 
AMPM_thing��5      $��AM��6(   
�������$
��Left=�6I   $��
433,37,0,0q�6d   $	��Top=�6x   $��48(�6�   $��Microsoft JhengHei)�6�   �� 5      -   5        $ DayDate��5      
�������$��Munday, March 66, 2917��68   $
��Left=�6Y   $��294,133,0,0q�6t   $	��Top=�6�   $��22(�6�   5      �� 5   
   -   5         ��Name$ Sidebar_Back��5      
���   �$y�Hidden=�67    J� Opacity$	 0.3Q�6K   $
��Left=�6Y   $	��689��6t   
���   �$	��Top=�6�   $	��262��6�   �� 5   
   -	   5        $ SidebarH��5      
���   �$y�Hidden=�63   $	 0.8Q�6G   $
��Left=�6U   $��60��6p   
���   �$	��Top=�6�   $	��262��6�   �� 5   
   -
   5        $ label��5      
�������$��The Dining Philosophers��63   $
��Left=�6U   $��938,622,0,0q�6p   $	��Top=�6�   $��24(�6�   $��Vladimir Script)�6�   �� 5   
   -   5         ��Name$ alarmSideBarButton��5      
������ $
��Left=�6F   $��	15,15,0,0q�6a   $	��Top=�6t   $��30��6�   $��30��6�   $��22(�6�   $��0q�6�   .��5      �� $1��+pack://application:,,,/Images/menu-icon.png��6   $
 Fant=�6I   6r   5      �� 5   
   $
��Auto=�6   
������ $	��252��6S   $��
10,65,0,10q�6_   $
��Left=�6s   .��5      �� -   5        	 ��Name$	 AlarmList_Panel��5      $y�Hidden=�62   $
��Left=�6F   $	��Top=�6a   $	��235��6y   $
��Auto��6�   5      �� 5   
   -   5        $ AddAlarmButton��5      $y�Hidden=�6)   
������ $
��Left=�6V   $��
180,15,0,0q�6q   $	��Top=�6�   $��30��6�   $��30��6�   $��0q�6�   .��5      �� $9��3pack://application:,,,/Images/plus-button-green.png��6   $��50��6Q   6_   5       �� 5!   
   -   5        $ SettingsButton��5!      
������ $y�Hidden=�6B   $��0q�6V   $
��Left=�6j   $��
220,15,0,0q�6�   $	��Top=�6�   $��30��6�   $��30��6�   .��5"      �� -   5        $ GlobalSettings_Button��5"      $9��3pack://application:,,,/Images/TurnMyWrenchBabay.png��64   6t   5#      �� 5$   
   -   5        $ missedAlsarmLabel��5$      
�������$��You Missed An Alarm��6>   $
��Left=�6\   $��384,138,0,0q�6w   $	��Top=�6�   $��22(�6�   $y�	Collapsed=�6�   � 5%   
   -   5        $ Debug_Options��5%      $��564,47,28,-103q�6&   $y�Hidden=�6>   .+�5&      �� ���	#FFF4F4F5$
��Left=�6)   $��92��6D   
���   �$	��Top=�6_   $	��262��6w   $��	348,0,0,0q�6�   �� 5'      -   5        $ label1��5'      $��Clock Speed��6$   
���   �$
��Left=�6M   $��
365,15,0,0q�6h   $	��Top=�6|   $��16(�6�   
 J� RenderTransformOrigin$
 -1.956,0.675&�6�   �� 5(      -   5         ��Name$ ClockSpeedSlider��5(      $��365,55,18,0q�6/    � Maximum$ 14Q�6q   �� 5)      -   5        $ addDayButton��5)      $��Add day��6+   $
��Left=�6=   $	��Top=�6X   $��74��6p   $��
513,26,0,0q�6{   6�   5+      �� 5,   
   -   5        $ Debug��5,      $��Show Debug ��6    $
��Left=�66   $��1085,22,0,0q�6Q   $	��Top=�6f   $��89��6~   6�   5.      5/      
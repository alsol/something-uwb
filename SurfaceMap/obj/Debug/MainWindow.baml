   M S B A M L   `   `   `  ����E  ASurfaceMap, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:SurfaceMap
SurfaceMap  5   	       SurfaceMap.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   R NOxyPlot.Wpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=75e952ba404cdbb0 oxyhttp://oxyplot.org/wpf  5   	   $localclr-namespace:SurfaceMap  5   	     � Title$  
MainWindow��5   	   $	��350��6   $	��525��6)   .��5      ]� .+�5   
   o� $	��Top=�6   
�������.@�5	      m� $��_File��6   .@�5
      m� $��_Open��6   m� 5      $��_Close��6   m� 5      $��_Save��6   5      m� 5      $��_About��6   5      5      � 5   
    � RowDefinitions 5      �� 5      $��40 �6    �� 5      $
��128* �6    �� 5      $
��auto �6    .+�5        OxyPlot.Wpf.PlotView  -   5        $��MainPlot��5      $��1��6+   $��	20,0,20,0q�68      Model 6K   �� *Model+�� 5      -   5        $��ConnectButton��5      $
��Left=�6*   $��10,8,0,8q�6E   $��75��6W    �� 	ClickMode$ Press=�6b   $��Connect��6t   6�   �� 5      -   5        $��
PlayButton��5      $��	0,8,211,8q�6'   $ Press=�6:   $
��Play��6L   $��Right=�6[   $��75��6w   {�.  6�   �� 5      -   5        $��
StopButton��5      $��	0,8,109,8q�6'   $ Press=�6:   $
��Stop��6L   $��Right=�6[   $��75��6w   {�.  6�   �� 5      -   5        $��
SaveButton��5      $��0,8,10,8q�6'   $ Press=�69   $
��Save��6K   $��Right=�6Z   $��75��6v   {�.  6�   �� 5!      $��3��6   ��5"      �� 5#      .R�5$      �  � ColumnDefinitions 5%      �� 5&   "   $��* �63   6?   �� 5'   "   $
��auto �63   6B   �� 5(   "   $
��auto �63   6B   5*      5+      .@�5-      �� -   5        $��Status��5-      	Not ready65   �� 5.      $��1��6    .��5/      �� -   5        $	��Idn��5/       Oscillocsope:6;   50      �� 51      $��2��6    .��52      �� -   5        $��ReadyMarker��52       
���  ��Not connected6S   53      54      56      58      59      
   M S B A M L   `   `   `  ���� E  ASurfaceMap, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:SurfaceMap
SurfaceMap      SurfaceMap.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     R NOxyPlot.Wpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=75e952ba404cdbb0 oxyhttp://oxyplot.org/wpf  $localclr-namespace:SurfaceMap    � Title$  
MainWindow��$	��350��$	��525��.��]� .+�o� $	��Top=�
�������.@�m� $��_File��.@�m� $��_Open��m� $��_Close��m� $��_Save��m� $��_About���  � RowDefinitions �� $��40 ��� $
��128* ��� $
��auto �.+�  OxyPlot.Wpf.PlotView  -   $��MainPlot��$��1��   Model �� *Model+�� -   $��ConnectButton��$
��Left=�$��10,8,0,8q�$��75�� �� 	ClickMode$ Press=�$��Connect���� -   $��
PlayButton��$��	0,8,211,8q�$ Press=�$
��Play��$��Right=�$��75��{�.  �� -   $��
StopButton��$��	0,8,109,8q�$ Press=�$
��Stop��$��Right=�$��75��{�.  �� -   $��
SaveButton��$��0,8,10,8q�$ Press=�$
��Save��$��Right=�$��75��{�.  �� $��3������ .R��  � ColumnDefinitions �� $��* ��� $
��auto ��� $
��auto �.@��� -   $��Status��	Not ready�� $��1��.���� -   $	��Idn��Oscillocsope:�� $��2��.���� -   $��ReadyMarker��
���  ��Not connected
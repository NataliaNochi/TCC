   M S B A M L   `   `   `  ����>  :TCC, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:TCC.Classes.BaseTCC.Classes.Base  5   	       TCC.Formularios.FrmFase2   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   h dMicrosoft.Kinect.Toolkit.Controls, Version=1.8.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e350k(http://schemas.microsoft.com/kinect/2013  5      'myclr-namespace:TCC.Classes.Base  5   	   -   5          �Name$  frmFase2��5   	    � Title$ FrmFase2��5   	   $	��490��6   $	��675��6'    � WindowState$ 	Maximized=�6L    � WindowStartupLocation$ CenterScreen=�6d   .��5
      � $��	0,0,0,0.5q�6    � ColumnDefinitions 5   
   �� 5      $��10 �6   �� 5      $
��287* �6   �� 5      $
��340* �6   �� 5      $��26.164 �6   �� 5      $��3.836 �6    � RowDefinitions 5   
   �� 5      $��10 �6   �� 5      �� 5      $��78 �6   .+�5   
   �� -   5         ��Name$ canvasKinect��5      $��2��6'   $��1��67   $��1,2,1,10q�6D   ���	#FFF5E6E66V   4  .Microsoft.Kinect.Toolkit.Controls.KinectRegion  5   
   -   5        $��kinectRegion��5      $��2��6-   $��8,7,2,0q�6:   $��68��6K   $	��Top=�6W   $��2��6o   .��5      ��  �� Orientation$ 
Horizontal=�6   .+�5      )   #TCC.Classes.Base.KinectToggleButton  -   5          Name$ btnEscalaCinza��5   (   )   #TCC.Classes.Base.KinectToggleButton	   Click$	 btnEscalaCinza_Click��6�   $��Escala Cinza��6@   $��67��6W   
�������$	��108��6v   $��18(�6�     5      -   5        $ btnEsqueletoUsuario��5   (   $	 btnEsqueletoUsuario_Click��6�   $��Esqueleto do Usuário��6E   $��67��6d   $��18(�6p   
�������$	��177��6�   6�   5       5!      �� 5#   
   -   5        $��slider��5#      
 �� Orientation$
 Vertical=�6    � Minimum$	 -27Q�66    � Maximum$ 27Q�6D    � SmallChange$ 1Q�6Q    � Value$ 0Q�6a    J� RenderTransformOrigin$ 0.519,0.472&�6�   $��4,0,-4,5q�6�   $��3��6  $��1��6  $��2��6(  ��6k   ��  �� ElementName$ imagemCamera�� �� Path$ ActualHeight�6(  �� 5$   
   -   5         ��Name$ 	lb_Angulo��5$      $��Ângulo Atual: 0��6#   $��2��6=   $��2��6M   $ 0.577,0.451&�6Z   $��2��6~   $��30��6�   $	��Top=�6�   $��Right=�6�   $	��105��6�   �� 5%   
   $��Tempo:��6   $��2��6!   $
��Left=�61   $��	0,35,0,11q�6L   $��2��6_   $��16(�6l   $��67��6z   �� 5&   
   -   5        $ 	lb_Pontos��5&      $��0��6#   $��2��6/   $��237,22,2,10q�6?   $��2��6T   $��30(�6a   $
��Bold"�6o   $��3��6�   �� 5'   
   -	   5         ��Name$ tb_timer��5'      $��2��6'   $
��Left=�67   $��
70,36,0,16q�6R   $��2��6f   $
��Wrap=�6s   $��20(�6�   $��93��6�   $
��Bold"�6�   �� 6�    � Language$ pt-br�6�   $�00:00:00��6�   6�   �� 5(   
   $��Pontos:��6   $��2��6"   $��167,35,106,11q�62   $��2��6I   $��16(�6V   �� 5)   
   -
   5        $
��pose��5)      $��1��6   $
��Left=�6,   $	��364��6G   $��1��6T   $	��Top=�6a   $	��278��6y   5+      5,      
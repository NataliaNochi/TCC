   M S B A M L   `   `   `  ����>  :TCC, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:TCC.Classes.BaseTCC.Classes.Base  5   	       TCC.Formularios.FrmFase1   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   h dMicrosoft.Kinect.Toolkit.Controls, Version=1.8.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e350k(http://schemas.microsoft.com/kinect/2013  5   	   'myclr-namespace:TCC.Classes.Base  5   	   -   5          �Name$  frmFase1��5   	    � Title$ FrmFase1��5
   	   $��939.881��6    � WindowStartupLocation$ CenterScreen=�6+    � 
ResizeMode$ NoResize=�6j    � WindowState$ 	Maximized=�6�   $��1256.903��6�   .��5      � -   5        $��gdGeral��5      $��10,10,4,8.5q�6    � RowDefinitions 5   
   �� 5      $
��299* �6   �� 5      $
��594* �6    � ColumnDefinitions 5   
   �� 5      .+�5   
   4  .Microsoft.Kinect.Toolkit.Controls.KinectRegion  -   5          Name$ kinectRegion��5       J� RenderTransformOrigin$ 0.5,0.5&�6/   $��2��6O   .��5      �� -   5        	 ��Name$	 canvasRegion��5      $
��1237��6+   $��
0,-31,0,31q�68   .+�5      �� -   5        
 ��Name$
 	stkKinect��5      ���#F3F3FA �� Left$	 826��6F   
 �� Top$	 177��6X   $	��149��6i   $	��149��6v   �� 5      -   5        $��imgVida��5      $	��100��6'   $	��100��64   $ 88��6@   $	 226��6Q   )   #TCC.Classes.Base.KinectToggleButton  5      -   5          Name$ btnPosturaIncorreta��5   (   )   #TCC.Classes.Base.KinectToggleButton   Click$ btnPosturaIncorreta_Click��6�   {�.  $	��Top=�6X   
�������$ -0.434,0.461&�6�   $	��243��6�   $	��197��6�   $	 412��6�   $ 89��6�   .��5      �� -   5         ��Name$ 	imgErrado��5      $
��Fill=�6/   5        5      -	   5        $ btnPosturaCorreta��5   (   $ btnPosturaCorreta_Click��6�   {�.  $	��Top=�6V   
�������$ 
0.44,0.518&�6�   $	��243��6�   $	��197��6�   $	 617��6�   $ 89��6�   .��5      �� -
   5        $ imgCerto��5      $
��Fill=�6.   5        5!      -   5        $ btnPostura1��5!   (   $ btnPostura1_Click��6�   $	��Top=�6?   
�������$ 
0.44,0.518&�6n   $	��243��6�   $	��200��6�   $	 359��6�   $ 6��6�   .��5"      �� -   5        $ 	img1Certo��5"      $1��+/TCC;component/Imagens/Fase1/img1_certo.png��6/   $
��Fill=�6d   5#        5%      -   5        $ btnPostura2��5%   (   $ btnPostura2_Click��6�   $	��Top=�6?   
�������$ 
0.44,0.518&�6n   $	��243��6�   $	��200��6�   $	 209��6�   $	 359��6�   .��5&      �� -   5        $ 	img2Certo��5&      $1��+/TCC;component/Imagens/Fase1/img2_certo.png��6/   $
��Fill=�6d   5'        5)      -   5        $ btnPostura3��5)   (   $ btnPostura3_Click��6�   $	��Top=�6?   
�������$ 
0.44,0.518&�6n   $	��243��6�   $	��200��6�   $	 412��6�   $	 359��6�   .��5*      �� -   5        $ 
img3Errado��5*      $2��,/TCC;component/Imagens/Fase1/img3_errado.png��60   $
��Fill=�6f   5+        5-      -   5        $ btnPostura4��5-   (   $ btnPostura4_Click��6�   $	��Top=�6?   
�������$ 0.5,0.5&�6n   $	��243��6�   $	��200��6�   $	 614��6�   $	 359��6�   .��5.      �� -   5        $ 
img2Errado��5.      $2��,/TCC;component/Imagens/Fase1/img2_errado.png��60   $
��Fill=�6f   5/        51      -   5        $ btnPostura5��51   (   $ btnPostura5_Click��6�   $	��Top=�6?   
�������$ 
0.44,0.518&�6n   $	��243��6�   $	��200��6�   $ 6��6�   $	 615��6�   .��52      �� -   5        $ 	img3Certo��52      $1��+/TCC;component/Imagens/Fase1/img3_certo.png��6/   $
��Fill=�6d   53        55      -   5        $ btnPostura6��55   (   $ btnPostura6_Click��6�   $	��Top=�6?   
�������$ 
0.44,0.518&�6n   $	��243��6�   $	��200��6�   $	 209��6�   $	 615��6�   .��56      �� -   5        $ 
img1Errado��56      $2��,/TCC;component/Imagens/Fase1/img1_errado.png��60   $
��Fill=�6f   57        59      -   5        $ btnPostura7��59   (   $ btnPostura7_Click��6�   $	��Top=�6?   
�������$ 
0.44,0.518&�6n   $	��243��6�   $	��200��6�   $	 413��6�   $	 615��6�   .��5:      �� -   5        $ 
img4Errado��5:      $2��,/TCC;component/Imagens/Fase1/img4_errado.png��60   $
��Fill=�6f   5;        5=      -   5        $ btnPostura8��5=   (   $ btnPostura8_Click��6�   $	��Top=�6?   
�������$ 
0.44,0.518&�6n   $	��243��6�   $	��200��6�   $	 614��6�   $	 615��6�   .��5>      �� -   5        $ 	img4Certo��5>      $1��+/TCC;component/Imagens/Fase1/img4_certo.png��6/   $
��Fill=�6d   5?      �� 5A      -   5         ��Name$ lbPontos��5A      $	 144��6*   $��57��6;   $��98��6G   $��36(�6R   $��0��6`   $	 249��6l     5C      -   5        $ btnPostura9��5C   (   $ btnPostura9_Click��6�   $	��Top=�6?   
�������$ 0.5,0.5&�6n   $	��243��6�   $	��200��6�   $	 819��6�   $	 359��6�   .��5D      �� -   5        $ 
img5Errado��5D      $2��,/TCC;component/Imagens/Fase1/img5_errado.png��60   $
��Fill=�6f   5E        5G      -   5        $ btnPostura11��5G   (   $ btnPostura11_Click��6�   $	��Top=�6@   
�������$ 
0.44,0.518&�6o   $	��243��6�   $	��200��6�   $	 826��6�   $	 615��6�   .��5H      �� -   5        $ 	img5Certo��5H      $1��+/TCC;component/Imagens/Fase1/img5_certo.png��6/   $
��Fill=�6d   5I        5K      -    5        $ btnPostura10��5K   (   $ btnPostura10_Click��6�   $	��Top=�6@   
�������$ 0.5,0.5&�6o   $	��243��6�   $	��200��6�   $
 1024��6�   $	 359��6�   .��5L      �� -!   5        $ 
img6Errado��5L      $2��,/TCC;component/Imagens/Fase1/img6_errado.png��60   $
��Fill=�6f   5M        5O      -"   5        $ btnPostura12��5O   (   $ btnPostura12_Click��6�   $	��Top=�6@   
�������$ 
0.44,0.518&�6o   $	��243��6�   $	��200��6�   $
 1024��6�   $	 615��6�   .��5P      �� -#   5        $ 	img6Certo��5P      $1��+/TCC;component/Imagens/Fase1/img6_certo.png��6/   $
��Fill=�6d   5Q      �� 5S      -$   5        $ lbPontos_Copy��5S      $	 144��6/   $��57��6@   $	��191��6L   $��36(�6X   $��Pontuação:��6f   $ 36��6{   5T      5U      5V      5W      
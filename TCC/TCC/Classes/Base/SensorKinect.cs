using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Kinect;

namespace TCC.Classes.Base
{
    public class SensorKinect //KinectSensor
    {
        public KinectSensor kinect;

        public void AnguloElevacao(int anguloElevacao)
        {
            try
            {
                kinect.ElevationAngle = anguloElevacao; //ajusta o ângulo de elevação do sensor     
            }
            catch
            {
                MessageBox.Show("Ocorreu um erro durante a execução, verifique se o sensor encontra-se conectado");
            }
        }

        public void DesligarSensor()
        {
            if (kinect != null)
            {
                AnguloElevacao(0);
                kinect.Stop(); //Para o sensor
                kinect.AudioSource.Stop(); //Para o audio 
                kinect.Dispose();
            }
            else       
                MessageBox.Show("Verifique se o sensor encontra-se conectado");
        }

        #region Inicializar Sensor
        public void InicializarSensor()
        {
            try
            {
                if (KinectSensor.KinectSensors.Count > 0) //Verifica se há algum sensor conectado
                {
                    //Seta o objeto kinectSensor como o primeiro sensor conectado (podendo haver mais de um sensor conectado, porém nessa aplicação será utilizado apenas um)
                    kinect = KinectSensor.KinectSensors[0];

                    if (kinect.Status == KinectStatus.Connected)
                    {
                        kinect.ColorStream.Enable(); //Inicializa fluxo de cores

                        //Ativando os controles de câmera e profundidade
                        kinect.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);
                        kinect.DepthStream.Enable();
                        kinect.Start();

                        AnguloElevacao(10);
                        kinect.SkeletonStream.Enable();

                    }
                }
                else
                {
                    MessageBox.Show("Erro: Verifique se o dispositivo kinect está conectado ao computador \n");
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro: Verifique se o dispositivo kinect está conectado ao computador \n" + erro.Message);
                return;
            }
        }
        #endregion

    }
}

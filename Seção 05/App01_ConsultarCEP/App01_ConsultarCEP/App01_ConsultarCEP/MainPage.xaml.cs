﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using App01_ConsultarCEP.Servico;
using App01_ConsultarCEP.Servico.Modelo;

namespace App01_ConsultarCEP
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BOTAO.Clicked += BuscarCEP;
        }

        private void BuscarCEP(object sender, EventArgs e)
        {
            string cep = CEP.Text.Trim();

            if (isValidCEP(cep))
            {
                try
                {
                    Endereco end = ViaCEPServico.BuscarEnderecoViaCEP(cep);
                    if (end != null) RESULTADO.Text = string.Format("Endereço: {2} de {3} {0},{1} ", end.localidade, end.uf, end.logradouro, end.bairro);
                    else DisplayAlert("ERRO", "O endereço não foi encontrado para o CEP informado: " + cep, "OK");
                }
                catch(Exception ex)
                {
                    DisplayAlert("ERRO CRÍTICO", ex.Message, "OK");
                }
            }
        }

        private bool isValidCEP(string cep)
        {
            Boolean valid = true;

            if (cep.Length != 8)
            {
                DisplayAlert("ERRO", "CEP inválido! O CEP deve conter 8 caracteres", "OK");
                valid = false;
            }

            int NovoCEP = 0;

            if (!int.TryParse(cep,out NovoCEP))
            {
                DisplayAlert("ERRO", "CEP inválido! O CEP deve composto apenas por números", "OK");
                valid = false;
            }

            return valid;
        }
    }
}

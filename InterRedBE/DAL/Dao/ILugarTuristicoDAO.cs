﻿using InterRedBE.DAL.DTO;
using InterRedBE.DAL.Models;
using InterRedBE.UTILS;
using InterRedBE.UTILS.Services;

namespace InterRedBE.DAL.Dao
{
    public interface ILugarTuristicoDAO : ICRUD<LugarTuristico>
    {
        public OperationResponse<ListaEnlazadaDoble<LugarTuristicoConVisitasDTO>> GetTop10Visitas();
        public OperationResponse<ListaEnlazadaDoble<LugarTuristicoConPromedioDTO>> GetTop10ByRating();
        Task<OperationResponse<ListaEnlazadaDoble<LugarTuristicoDTO>>> GetByDepartamentoId(int idDepartamento);

    }
}

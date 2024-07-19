using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardaFeitico : MonoBehaviour
{
    [System.Serializable]
    public class FeiticoData
    {
        public Feiticos feitico;
        public KeyCode atalho;
        public FeiticoEstado estado;
        public float tempoRecargaAtual;
        public float tempoDuracaoAtual;
    }

    public List<FeiticoData> feiticos;

    public enum FeiticoEstado
    {
        pronto,
        ativo,
        recarga
    }

    private void Update()
    {
        foreach (var fd in feiticos)
        {
            switch (fd.estado)
            {
                case FeiticoEstado.pronto:
                    if (Input.GetKeyDown(fd.atalho))
                    {
                        fd.feitico.Ativar(gameObject);
                        fd.estado = FeiticoEstado.ativo;
                        fd.tempoDuracaoAtual = fd.feitico.tempoDuracao;
                    }
                    break;
                case FeiticoEstado.ativo:
                    if (fd.tempoDuracaoAtual > 0)
                    {
                        fd.tempoDuracaoAtual -= Time.deltaTime;
                    }
                    else
                    {
                        fd.feitico.RecargaComecar(gameObject);
                        fd.estado = FeiticoEstado.recarga;
                        fd.tempoRecargaAtual = fd.feitico.tempoRecarga;
                    }
                    break;
                case FeiticoEstado.recarga:
                    if (fd.tempoRecargaAtual > 0)
                    {
                        fd.tempoRecargaAtual -= Time.deltaTime;
                    }
                    else
                    {
                        fd.estado = FeiticoEstado.pronto;
                    }
                    break;
            }
        }
    }
}

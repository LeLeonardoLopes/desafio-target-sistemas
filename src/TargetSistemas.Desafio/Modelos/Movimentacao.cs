namespace TargetSistemas.Desafio.Modelos;

public class Movimentacao
{
    public int Id { get; set; }
    public int CodigoProduto { get; set; }
    public string DescricaoProduto { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public int Quantidade { get; set; }
    public int EstoqueAnterior { get; set; }
    public int EstoqueFinal { get; set; }
}

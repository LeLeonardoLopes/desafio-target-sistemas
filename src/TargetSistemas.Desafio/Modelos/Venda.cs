namespace TargetSistemas.Desafio.Modelos;

public class Venda
{
    public string Vendedor { get; set; } = string.Empty;
    public decimal Valor { get; set; }
}

public class DadosVendas
{
    public List<Venda> Vendas { get; set; } = new List<Venda>();
}

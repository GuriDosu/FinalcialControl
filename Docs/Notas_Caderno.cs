/*
 Notas do Projeto - "FinalcialControl" (Caderno de anotações - versão detalhada)
 Autor: Gerado por GitHub Copilot
 Objetivo: Fornecer um arquivo de consulta (não compilável) com os principais modelos e explicações detalhadas
            "escrevendo em cima" dos próprios modelos (linha a linha) para uso como caderno pedagógico.

 Observação: Este arquivo é 100% comentário. Abra os arquivos reais para código executável.

================================================================================
Sumário rápido (o que contém este caderno):
- Exemplo comentado de entidade `Transaction` (modelo persistido)
- Exemplo comentado de DTO `CreateTransactionDTO` (entrada da API)
- Exemplo comentado de interface `ITransactionService`
- Trecho comentado do `TransactionController` explicando decisões de rota/retorno/erros
- Exemplos de payload JSON e sugestões práticas (Validação, Status HTTP, Boas práticas)

================================================================================
1) Entidade: Transaction (modelo persistido)
================================================================================
Abaixo está um exemplo de como a entidade pode ser escrita. Cada linha tem anotações como se você estivesse
escrevendo no caderno, explicando o propósito e decisões de modelagem.

/*
public class Transaction
{
    // Id: chave primária da entidade. Em bancos relacionais, geralmente gerada automaticamente (IDENTITY / auto-increment).
    // Serve para identificar o registro de forma única.
    public int Id { get; set; }

    // Amount (valor): use decimal para valores monetários (evite float/double por precisão).
    // - Normalmente você define precisão/escala no banco (ex: decimal(18,2)).
    // - Em validações: garantir que seja maior que zero para transações de débito, por exemplo.
    public decimal Amount { get; set; }

    // Description: texto livre para explicar a transação.
    // - Pode usar [StringLength(250)] ou similar para limitar no modelo/DB.
    // - Opcional: campos para notas privadas ou categorias estruturadas.
    public string Description { get; set; }

    // Date: data e hora da transação.
    // - Guarde em UTC se o sistema for distribuído; se for local, defina claramente a convenção usada.
    public DateTime Date { get; set; }

    // CategoryId: exemplo de FK para outra tabela (se houver categorias).
    // - Pode ser null (transação sem categoria) ou obrigatório conforme regras de negócio.
    public int? CategoryId { get; set; }

    // Navigation property (opcional no DB context) para facilitar queries via EF Core.
    // public Category Category { get; set; }
}
*/

Notas de caderno sobre a entidade:
- Separar entidade (persistência) do DTO evita expor detalhes internos (timestamps, FK internas, etc.).
- Pense quais campos são mutáveis e quais são apenas leitura (ex: CreatedAt somente leitura).

================================================================================
2) DTO: CreateTransactionDTO (entrada da API)
================================================================================
O DTO define apenas os dados necessários para criar uma transação. Anotações mostram validações comuns.

/*
public class CreateTransactionDTO
{
    // Use DataAnnotations para validação automática com [ApiController].
    // Ex.: [Required] faz com que o framework modele ModelState inválido automaticamente.

    // Valor da transação. Não use string para valores monetários.
    // [Required]
    // [Range(0.01, 1000000)]
    public decimal Amount { get; set; }

    // Descrição curta - opcional, mas útil para o usuário entender a origem da transação.
    // [StringLength(250)]
    public string? Description { get; set; }

    // Data da transação: se não passada, o serviço pode assumir DateTime.UtcNow.
    // [Required]
    public DateTime Date { get; set; }

    // Categoria opcional: referencia apenas por Id para simplificar a API.
    public int? CategoryId { get; set; }
}
*/

Dicas sobre DTOs:
- Sempre validar e normalizar dados no service (ex: arredondamento, conversão de timezone).
- Preferir DTOs por operação (CreateTransactionDTO, UpdateTransactionDTO, TransactionResponseDTO).

================================================================================
3) Interface: ITransactionService (contrato)
================================================================================
A interface declara o que o controller pode esperar do serviço. Mantê-la pequena e clara facilita testes.

/*
public interface ITransactionService
{
    // Retorna todas as transações (pode retornar DTOs ou entidades dependendo da camada).
    IEnumerable<Transaction> GetTransactions();

    // Adiciona uma nova transação baseada no DTO de entrada.
    // - Pode lançar ArgumentException se os dados forem inválidos.
    // - Pode lançar InvalidOperationException se houver um estado que impeça a criação.
    Transaction Add(CreateTransactionDTO dto);
}
*/

Boas práticas:
- Use tipos de retorno que facilitem testes (por exemplo, retornar DTOs em vez de entidades dentro do serviço quando o controller os consome).
- Considerar retornar Result/Response objects (por exemplo, OperationResult<T>) para não depender de exceções para fluxo normal.

================================================================================
4) Trecho comentado do Controller: TransactionController.cs
================================================================================
Aqui está o fluxo do controller explicado "no caderno". Reforço decisões sobre códigos HTTP e tratamento de exceções.

/*
[ApiController]
[Route("api/[controller]")]
public class TransactionController : Controller
{
    private readonly ITransactionService _transactionService;

    public TransactionController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    // GET api/transaction
    [HttpGet]
    public IActionResult GetTransactions()
    {
        try
        {
            // Obtém do serviço. O serviço é responsável por regras (filtrar, ordenar, mapear DTOs se necessário).
            var transactions = _transactionService.GetTransactions();
            // Retorna 200 OK com a lista no corpo da resposta.
            return Ok(transactions);
        }
        catch (InvalidOperationException ex)
        {
            // Se algo específico aconteceu (ex: recurso não encontrado), devolvemos 404.
            // Observação: normalmente GetAll não lança 404; esse catch está aqui para ilustrar tratamento.
            return NotFound(ex.Message);
        }
    }

    // POST api/transaction
    [HttpPost]
    public IActionResult Create([FromBody] CreateTransactionDTO transaction)
    {
        // Como usamos [ApiController], a validação automática do ModelState ocorre antes de entrar aqui.
        try
        {
            // O serviço valida e cria a entidade.
            var transactionCreated = _transactionService.Add(transaction);

            // Boa prática REST: retornar 201 Created com Location para o recurso criado.
            // Exemplo:
            // return CreatedAtAction(nameof(GetById), new { id = transactionCreated.Id }, transactionCreated);

            // Para simplicidade (e compatibilidade com seu código atual) retorna 200 OK.
            return Ok(transactionCreated);
        }
        catch (ArgumentException ex)
        {
            // Dados inválidos -> 400 Bad Request
            return BadRequest(ex.Message);
        }
    }
}
*/

Observações de caderno:
- Preferir CreatedAtAction em POST para seguir a semântica REST (201 + Location).
- Evitar usar exceções para controle de fluxo; prefira um objeto de resultado quando apropriado.

================================================================================
5) Exemplos práticos (JSON e cenários)
================================================================================
Exemplo de payload para criar uma transação (POST /api/transaction):
{
  "amount": 100.50,
  "description": "Compra no mercado",
  "date": "2026-04-07T15:30:00Z",
  "categoryId": 3
}

Cenários de validação (o que checar no serviço):
- Amount <= 0 -> rejeitar (ArgumentException com mensagem clara)
- Date muito no futuro -> validar conforme regras de negócio
- CategoryId inválido -> checar existência antes de atribuir

================================================================================
6) Sugestões de melhoria (práticas do caderno)
================================================================================
- Logging: injetar ILogger<T> nos controllers e serviços para registrar eventos e exceções.
- Middleware de Exceptions: criar um middleware global para capturar exceções e padronizar respostas JSON de erro.
- Mapear entidades para DTOs com AutoMapper para manter o serviço limpo.
- Testes: criar testes unitários para ITransactionService e testes de integração para controllers (WebApplicationFactory).

================================================================================
7) Passos seguintes sugeridos (tarefas de caderno)
================================================================================
- Verificar se `ITransactionService` está registrado em Program.cs (AddScoped ou AddTransient).
- Criar `TransactionResponseDTO` para separar o retorno do controller da entidade do DB.
- Implementar CreatedAtAction no POST e endpoint GET por id (GetById) para retornar 201 corretamente.

================================================================================
8) Conclusão
================================================================================
Este arquivo foi pensado para ser uma referência de aprendizado: leia como se fosse anotações em um caderno.
Se quiser, posso:
- Inserir comentários semelhantes diretamente nos arquivos de código reais (edits que alteram arquivos .cs do projeto),
- Gerar um README.md com estas seções em Markdown,
- Criar DTOs/responses reais e aplicar CreatedAtAction no controller.

*/

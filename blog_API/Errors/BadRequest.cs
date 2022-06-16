namespace blog_API.Errors {
    public class BadRequest : Exception {
        private string mensagem;
        public BadRequest(string mensagem) {
            this.mensagem = mensagem;
        }

        public string GetMensagem() {
            return this.mensagem;
        }
    }
}

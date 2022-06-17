namespace blog_API.Errors {
    public class IntegrationException : Exception {
        private string mensagem;
        public IntegrationException(string mensagem) {
            this.mensagem = mensagem;
        }

        public string GetMensagem() {
            return this.mensagem;
        }
    }
}

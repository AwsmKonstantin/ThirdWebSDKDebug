using System.Threading.Tasks;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;

namespace Thirdweb.Wallets
{
    public class ThirdwebLocalWallet : IThirdwebWallet
    {
        private Account _account;
        private Web3 _web3;
        private WalletProvider _provider;
        private WalletProvider _signerProvider;

        public ThirdwebLocalWallet()
        {
            _account = null;
            _web3 = null;
            _provider = WalletProvider.LocalWallet;
            _signerProvider = WalletProvider.LocalWallet;
        }

        public Task<string> Connect(WalletConnection walletConnection, string rpc)
        {
            _account = Utils.UnlockOrGenerateLocalAccount(walletConnection.chainId, walletConnection.password, null);
            _web3 = new Web3(_account, rpc);
            return Task.FromResult(_account.Address);
        }

        public Task Disconnect()
        {
            _account = null;
            _web3 = null;
            return Task.CompletedTask;
        }

        public Account GetLocalAccount()
        {
            return _account;
        }

        public Task<string> GetAddress()
        {
            var addy = _account?.Address;
            if (addy != null)
                addy = addy.ToChecksumAddress();
            return Task.FromResult(addy);
        }

        public async Task<string> GetSignerAddress()
        {
            return await GetAddress();
        }

        public WalletProvider GetProvider()
        {
            return _provider;
        }

        public WalletProvider GetSignerProvider()
        {
            return _signerProvider;
        }

        public Task<Web3> GetWeb3()
        {
            return Task.FromResult(_web3);
        }

        public Task<Web3> GetSignerWeb3()
        {
            return Task.FromResult(_web3);
        }

        public Task<bool> IsConnected()
        {
            return Task.FromResult(_web3 != null);
        }
    }
}

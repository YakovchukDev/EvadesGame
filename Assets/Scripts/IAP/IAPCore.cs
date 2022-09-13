using System;
using Menu.Settings;
using TMPro;
using UnityEngine;
using UnityEngine.Purchasing;

namespace IAP
{
    public class IAPCore : MonoBehaviour, IStoreListener
    {
        [SerializeField] private GameObject _buyRusButton;
        [SerializeField] private SetLanguage _setLanguage;
        [SerializeField] private TMP_Text _coinText;
        private static IStoreController m_StoreController;
        private static IExtensionProvider m_StoreExtensionProvider;

        public static string LanguageRUS = "LanguageRUS";
        public static string coin100 = "coin100";
        public static string coin500 = "coin500";
        public static string coin1000 = "coin1000";
        public static string coin5000 = "coin5000";
        public static string coin10000 = "coin10000";
        public static string coin25000 = "coin25000";
        public static string coin50000 = "coin50000";

        private void Awake()
        {
            if (PlayerPrefs.HasKey("RUS"))
            {
                Destroy(_buyRusButton);
            }
        }

        void Start()
        {
            if (m_StoreController == null)
            {
                InitializePurchasing();
            }
        }

        public void InitializePurchasing()
        {
            if (IsInitialized())
            {
                return;
            }

            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

            builder.AddProduct(LanguageRUS, ProductType.NonConsumable);
            builder.AddProduct(coin100, ProductType.Consumable);
            builder.AddProduct(coin500, ProductType.Consumable);
            builder.AddProduct(coin1000, ProductType.Consumable);
            builder.AddProduct(coin5000, ProductType.Consumable);
            builder.AddProduct(coin10000, ProductType.Consumable);
            builder.AddProduct(coin25000, ProductType.Consumable);
            builder.AddProduct(coin50000, ProductType.Consumable);

            UnityPurchasing.Initialize(this, builder);
        }

        public void BuyRUS()
        {
            BuyProductID(LanguageRUS);
        }

        public void BuyCoins(string id)
        {
            BuyProductID(id);
        }


        void BuyProductID(string productId)
        {
            if (IsInitialized())
            {
                Product product = m_StoreController.products.WithID(productId);

                if (product != null && product.availableToPurchase)
                {
                    Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                    m_StoreController.InitiatePurchase(product); //покупаем
                }
                else
                {
                    Debug.Log(
                        "BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
                }
            }
            else
            {
                Debug.Log("BuyProductID FAIL. Not initialized.");
            }
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
        {
            if (String.Equals(args.purchasedProduct.definition.id, LanguageRUS, StringComparison.Ordinal))
            {
                Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));

                //действия при покупке
                if (PlayerPrefs.HasKey("RUS") == false)
                {
                    PlayerPrefs.SetInt("RUS", 0);
                    Destroy(_buyRusButton);
                    _setLanguage.Set("Russian");
                }
            }
            else if (String.Equals(args.purchasedProduct.definition.id, coin100, StringComparison.Ordinal))
            {
                Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 100);
                _coinText.text = PlayerPrefs.GetInt("Coins").ToString();
            }
            else if (String.Equals(args.purchasedProduct.definition.id, coin500, StringComparison.Ordinal))
            {
                Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 500);
                _coinText.text = PlayerPrefs.GetInt("Coins").ToString();
            }
            else if (String.Equals(args.purchasedProduct.definition.id, coin1000, StringComparison.Ordinal))
            {
                Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 1000);
                _coinText.text = PlayerPrefs.GetInt("Coins").ToString();
            }
            else if (String.Equals(args.purchasedProduct.definition.id, coin5000, StringComparison.Ordinal))
            {
                Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 5000);
                _coinText.text = PlayerPrefs.GetInt("Coins").ToString();
            }
            else if (String.Equals(args.purchasedProduct.definition.id, coin10000, StringComparison.Ordinal))
            {
                Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 10000);
                _coinText.text = PlayerPrefs.GetInt("Coins").ToString();
            }
            else if (String.Equals(args.purchasedProduct.definition.id, coin25000, StringComparison.Ordinal))
            {
                Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 25000);
                _coinText.text = PlayerPrefs.GetInt("Coins").ToString();
            }
            else if (String.Equals(args.purchasedProduct.definition.id, coin50000, StringComparison.Ordinal))
            {
                Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 50000);
                _coinText.text = PlayerPrefs.GetInt("Coins").ToString();
            }
            else
            {
                Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'",
                    args.purchasedProduct.definition.id));
            }

            return PurchaseProcessingResult.Complete;
        }

        public void RestorePurchases() //Восстановление покупок (только для Apple). У гугл это автоматический процесс.
        {
            if (!IsInitialized())
            {
                Debug.Log("RestorePurchases FAIL. Not initialized.");
                return;
            }

            if (Application.platform == RuntimePlatform.IPhonePlayer ||
                Application.platform == RuntimePlatform.OSXPlayer) //если запущенно на эпл устройстве
            {
                Debug.Log("RestorePurchases started ...");

                var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();

                apple.RestoreTransactions((result) =>
                {
                    Debug.Log("RestorePurchases continuing: " + result +
                              ". If no further messages, no purchases available to restore.");
                });
            }
            else
            {
                Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
            }
        }


        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            Debug.Log("OnInitialized: PASS");
            m_StoreController = controller;
            m_StoreExtensionProvider = extensions;
        }

        private bool IsInitialized()
        {
            return m_StoreController != null && m_StoreExtensionProvider != null;
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}",
                product.definition.storeSpecificId, failureReason));
        }
    }
}
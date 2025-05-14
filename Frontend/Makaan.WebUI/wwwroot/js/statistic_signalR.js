$(document).ready(function () {
    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/statistichub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    async function start() {
        try {
            await connection.start();
            console.log("Hub ile bağlantı kuruldu!");
        } catch (err) {
            console.log("Hub ile bağlantı kurulamadı! Tekrar denenecek...", err);
            setTimeout(() => start(), 3000); // 3 saniye sonra yeniden bağlanmayı dene
        }
    }

    // Bağlantı kapandığında yeniden bağlanmayı başlat
    connection.onclose(async () => {
        console.log("Bağlantı koptu! Yeniden bağlanmaya çalışılıyor...");
        await start();
    });

    //Mulk Eklendiginde fiyat dagilimini dinleme
    connection.on("ReceivePriceForAdmin", (status, price) => {
        // Bar chart referanslarını alın
        const rentChart = Chart.getChart('barChart'); // Kira grafiği
        const saleChart = Chart.getChart('bar2Chart'); // Satılık grafiği

        if (status === "Kira") {
            // En düşük ve en yüksek kira değerlerini güncelle
            const currentMinRent = rentChart.data.datasets[0].data[0];
            const currentMaxRent = rentChart.data.datasets[0].data[1];

            rentChart.data.datasets[0].data[0] = Math.min(currentMinRent, price); // En düşük kira
            rentChart.data.datasets[0].data[1] = Math.max(currentMaxRent, price); // En yüksek kira

            // Kira grafiğini güncelle
            rentChart.update();
        }

        if (status === "Satılık") {
            // En düşük ve en yüksek satılık değerlerini güncelle
            const currentMinSale = saleChart.data.datasets[0].data[0];
            const currentMaxSale = saleChart.data.datasets[0].data[1];

            saleChart.data.datasets[0].data[0] = Math.min(currentMinSale, price); // En düşük satılık
            saleChart.data.datasets[0].data[1] = Math.max(currentMaxSale, price); // En yüksek satılık

            // Satılık grafiğini güncelle
            saleChart.update();
        }
    });

    //Mulk Sayisini dinleme
    connection.on("ReceivePropertiesCountForAdmin", (count) => {
        let currentCount = parseInt($("#statistic-propertycount").text());

        let newCount = currentCount + count;

        $("#statistic-propertycount").html(newCount);
    });

    //Mulk Tipi Sayisini dinleme
    connection.on("ReceivePropertyTypeCountForAdmin", (count) => {
        let currentCount = parseInt($("#statistic-propertyTypeCount").text());

        let newCount = currentCount + count;

        $("#statistic-propertyTypeCount").html(newCount);
    });

    //Emlakci Sayisini dinleme
    connection.on("ReceiveEstateAgentCountForAdmin", (count) => {
        let currentCount = parseInt($("#statistic-EstateAgentCount").text());

        let newCount = currentCount + count;

        $("#statistic-EstateAgentCount").html(newCount);
    });

    //Uye Sayisini dinleme
    connection.on("ReceiveMemberCountForAdmin", (count) => {
        let currentCount = parseInt($("#statistic-MemberCount").text());

        let newCount = currentCount + count;

        $("#statistic-MemberCount").html(newCount);
    });



    //connection.on("ReceivePriceUpdateForAdmin", (status, price,propertyId) => {

    //    // En dusuk ve en yuksek kira degerlerini her seferinde inputlardan al
    //    let currentMinRentId = $("#minPricedRent").attr("name");
    //    let currentMinRent = parseFloat($("#minPricedRent").val()); //300
    //    let secondMinPricedRent = parseFloat($("#secondMinPricedRent").val()); //400

    //    let highestPricedRent = parseFloat($("#highestPricedRent").val());

    //    // En dusuk ve en yuksek satilik degerlerini her seferinde inputlardan al
    //    let MinPricedSale = parseFloat($("#MinPricedSale").val());
    //    let HighestPricedSale = parseFloat($("#HighestPricedSale").val());

    //    if (status == "Kira") {

    //        if (propertyId == currentMinRentId) {

    //            if (currentMinRent < price) {

    //                $("#minPricedRent").val(Math.min(price, secondMinPricedRent));
    //            }
    //            else {
    //                $("#minPricedRent").val(price);
    //            }
    //        }
    //        else {
    //            $("#minPricedRent").val(secondMinPricedRent);
    //        }
            
            
    //    }
        

    //});


    // İlk bağlantı denemesi
    start();
});

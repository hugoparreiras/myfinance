 var chart = null;
 function LoadDraft(r, d) {
     if (chart != null) {
         chart.destroy();
     }
     const data = {
         labels: [
             'Receitas',
             'Despesas'
         ],
         datasets: [{
             label: 'Grafico de receitas e despesas',
             data: [r, d],
             backgroundColor: [
                 'rgb(0, 255, 0)',
                 'rgb(255, 99, 132)'
             ],
             hoverOffset: 1
         }]
     };
     const config = {
         type: 'bar',
         data: data
     };
     const ctx = document.getElementById('transactionsChart').getContext('2d');
     const transactionsChart = new Chart(ctx, config);
     chart = transactionsChart;
 }


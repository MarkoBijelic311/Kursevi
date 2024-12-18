// to get current year
function getYear() {
    var currentDate = new Date();
    var currentYear = currentDate.getFullYear();
    document.querySelector("#displayYear").innerHTML = currentYear;
}

getYear();


// client section owl carousel
$(".client_owl-carousel").owlCarousel({
    loop: true,
    margin: 20,
    dots: false,
    nav: true,
    navText: [],
    autoplay: true,
    autoplayHoverPause: true,
    navText: [
        '<i class="fa fa-angle-left" aria-hidden="true"></i>',
        '<i class="fa fa-angle-right" aria-hidden="true"></i>'
    ],
    responsive: {
        0: {
            items: 1
        },
        600: {
            items: 2
        },
        1000: {
            items: 2
        }
    }
});



/** google_map js **/
function myMap() {
    var mapProp = {
        center: new google.maps.LatLng(40.712775, -74.005973),
        zoom: 18,
    };
    var map = new google.maps.Map(document.getElementById("googleMap"), mapProp);
}

$(".info-item .btn").click(function(){
    $(".container").toggleClass("log-in");
  });
  $(".container-form .btn").click(function(){
    $(".container").addClass("active");
  });

  $('.form').find('input, textarea').on('keyup blur focus', function (e) {
  
    var $this = $(this),
        label = $this.prev('label');
  
        if (e.type === 'keyup') {
              if ($this.val() === '') {
            label.removeClass('active highlight');
          } else {
            label.addClass('active highlight');
          }
      } else if (e.type === 'blur') {
          if( $this.val() === '' ) {
              label.removeClass('active highlight'); 
              } else {
              label.removeClass('highlight');   
              }   
      } else if (e.type === 'focus') {
        
        if( $this.val() === '' ) {
              label.removeClass('highlight'); 
              } 
        else if( $this.val() !== '' ) {
              label.addClass('highlight');
              }
      }
  
  });
  
  $('.tab a').on('click', function (e) {
    
    e.preventDefault();
    
    $(this).parent().addClass('active');
    $(this).parent().siblings().removeClass('active');
    
    target = $(this).attr('href');
  
    $('.tab-content > div').not(target).hide();
    
    $(target).fadeIn(600);
    
  });

function submitData() {
    const form = document.getElementById('contact-form');

    const name = form.querySelector('#name').value;
    const email = form.querySelector('#email').value;
    const phone = form.querySelector('#phone').value;
    const message = form.querySelector('#message').value;
    const surname = form.querySelector('#company').value;
    const currentDate = new Date().toISOString();

    const korisnikJson = {
        "Ime": name,
        "Prezime": surname,
        "Email": email,
        "BrojTelefona": phone,
        "Poruke": [
            {
                "SadrzajPoruke": message,
                "DatumSlanja": "2024-12-17T10:00:00"
            },
            {
                "SadrzajPoruke": "Vaša prijava je uspešna.",
                "DatumSlanja": "2024-12-17T10:00:00"
            }
        ],
        "PrijaveNaKurseve": [
            {
                "KursID": 101,
                "DatumPrijave": "2024-12-17T10:00:00"
            },
            {
                "KursID": 102, 
                "DatumPrijave": "2024-12-17T10:00:00"
            }
        ]
    };


    fetch('/Home/Team', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(korisnikJson)
    })
        .then(response => response.json())
        .then(data => {
            console.log('Success:', data);
        })
        .catch(error => {
            console.error('Error:', error);
        });
}
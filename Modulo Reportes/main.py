#Importe de librerias usadas en todo el modulo de reportes
import sys
from PyQt5 import QtWidgets
from PyQt5.QtWidgets import QDialog, QApplication
from PyQt5.uic import loadUi
import pyrebase 
import matplotlib.pyplot as plt
import numpy as np 
from reportlab.lib.pagesizes import A4
from reportlab.pdfgen import canvas
from reportlab.lib.units import inch, mm
from reportlab.lib.utils import ImageReader
import random,math
import pathlib
from datetime import datetime

#Parametros de acceso a la base de datos en firebase obtenidos de la propia base de datos
firebaseConfig = {
    "apiKey": "AIzaSyCStKY5gKZ28i9hvx9tez9byonQgHoojm4",
    "authDomain": "appterapeuta.firebaseapp.com",
    "databaseURL": "https://appterapeuta-default-rtdb.firebaseio.com",
    "projectId": "appterapeuta",
    "storageBucket": "appterapeuta.appspot.com",
    "messagingSenderId": "836459726857",
    "appId": "1:836459726857:web:058a18d22934756c0190d8"
}

firebase = pyrebase.initialize_app(firebaseConfig) #Inicializar pyrebase con los datos de configuracion
db = firebase.database() #Establecer la base de datos en db
auth = firebase.auth() #Establecer la autenticacion de la base de datos en auth

#Se establecen variables de uso global para visualizacion de valores en consola y para almacenamiento recurrente
id = 'default' 
intentos = [] 
fallas = [] 
tiempoSobrante = [] 
tiempoSobranteE = [] 
ejercicios = [] 
ejerciciosE = []
nombresEjercicios = []
nombresErrores = [] 
configEjercicios = []
num2 = []
nombreDatos = []
pacienteN = ''
pacienteS = ''
numeroEjer = 0
nombreCompleto = ''
documentoPaciente = ''
userTerapeuta = ''

#La clase login es la encargada de efectuar las operaciones de la primera interfaz
class Login(QDialog):
    #La funcion __init__ inicializa los atributos, carga la interfaz, los campos de texto. Todo lo visible
    #en la interfaz
    def __init__(self):
        super(Login,self).__init__()
        loadUi("login.ui",self) #Carga la interfaz login.ui
        self.loginbutton.clicked.connect(self.loginfunction) #Activa el boton de iniciar sesion
        self.password.setEchoMode(QtWidgets.QLineEdit.Password) #Hace que la contraseña aparezca como asteriscos
        self.invalid.setVisible(False)
    
    #La funcion loginfunction extrae la informacion de los campos de texto y realiza una comparacion y validacion 
    #con la base de datos 
    def loginfunction(self):
        email=self.email.text() #Extrae la informacion de correo puesta
        password=self.password.text() #Extrae la informacion de contraseña puesta
        try: #Ejecuta lo que hay dentro de este try si es incorrecto el correo o contraseña, ejecuta el except
            user = auth.sign_in_with_email_and_password(email,password) #Usa el correo y la contraseña y valida usando la funcion de pyrebase
            global id
            id = user['localId']
            createacc=CreateAcc()
            widget.addWidget(createacc)
            widget.setCurrentIndex(widget.currentIndex()+1)
        except:
            self.invalid.setVisible(True) #Proyecta aviso de error de correo o contraseña

#La clase CreateAcc es la encargada de ejecutar todo lo que hay en la interfaz de generacion de reportes 
class CreateAcc(QDialog):

    #La funcion __init__ ejecuta todos los atributos, carga la interfaz, los campos de texto y los botones
    def __init__(self):
        super(CreateAcc,self).__init__()
        loadUi("createacc.ui",self)
        self.BtnGenerar.setVisible(False)
        self.BtnGenerar.clicked.connect(self.generarPDF)
        self.generarid.clicked.connect(self.mostrarDatos)
        self.BtnLimpiar.clicked.connect(self.LimpiarDatos)
        self.confirm.setVisible(False)
        self.datoconfi.setVisible(False)
        self.datofall.setVisible(False)
        self.BtnLimpiar.setVisible(True)
    
    #La funcion LimpiarDatos se encarga de volver a poner por defecto los datos limpiandolos y haciendo
    #que la interfaz se ponga por defecto
    def LimpiarDatos(self):
        #limpia la interfaz
        self.paciente.clear()
        self.confirm.setVisible(False)
        self.datoconfi.setVisible(False)
        self.datofall.setVisible(False)
        self.generarid.setVisible(True)
        self.BtnGenerar.setVisible(False)
        self.BtnLimpiar.setVisible(False)

        #Llama los datos globales 
        global nombresEjercicios
        global nombresErrores
        global configEjercicios 
        global pacienteDatos 
        global nombreDatos 
        global ejercicios 
        global num2 
        global intentos 
        global fallas 
        global tiempoSobrante 
        global tiempoSobranteE 
        global NomRe 
        global CanRe 
        global listaIntentos 
        global infoIntentoA 
        global infoIntentoF 
        global infoIntentoT 
        global infoIntentoAN 
        global infoIntentoFN 
        global infoIntentoTN 
        global infoIntentoANsobreTotal 
        global infoIntentoTNsobreTotal 

        #Limpiar los datos, incluyendo los globales colocandolos por defecto vacios
        nombresEjercicios = []
        nombresErrores = []
        configEjercicios = []
        pacienteDatos = []
        nombreDatos = []
        ejercicios = []
        num2 = []
        intentos = []
        fallas = []
        tiempoSobrante = []
        tiempoSobranteE = []
        NomRe = []
        CanRe = []
        listaIntentos = []
        infoIntentoA = []
        infoIntentoF = []
        infoIntentoT = []
        infoIntentoAN = []
        infoIntentoFN = []
        infoIntentoTN = []
        infoIntentoANsobreTotal = []
        infoIntentoTNsobreTotal = []
    
    #La funcion MostrarDatos establece datos para la estructura del pdf y muestra en consola para verificacion manual
    def mostrarDatos(self):
        #Si los datos son correctos genera informacion global para el pdf como la fecha, el nombre del paciente y demas
        #Si son incorrectos o no tiene datos, ejecuta el except que es un aviso en pantalla
        try:
            
            now = datetime.now() #Tomar datos de fecha
            dia = now.day #Extraer dia
            mes = now.month #Extraer mes
            año = now.year #Extraer año
            hora = now.hour #Extraer hora
            minuto = now.minute #Extraer minuto
            segundos = now.second #Extraer segundos

            #Un string que contiene el dia, mes, año, hora, minutos y segundos 
            fechastr = str(dia) + '-' + str(mes) + '-' + str(año) + ' ' + str(hora) + '_' + str (minuto) + '_' + str(segundos)

            #Al ser el dato de usuario correcto y tener informacion, hara visible el boton de generar pdf y quitara 
            #el boyon de extraccion de datos
            self.datoconfi.setVisible(True)
            self.datofall.setVisible(False)
            self.BtnGenerar.setVisible(True)
            self.generarid.setVisible(False)
            self.BtnLimpiar.setVisible(True)

            #Añade datos globales
            global paciente
            paciente = self.paciente.text() #Agrega el usuario al string paciente 
            global pacienteN
            pacienteN = 'Reporte ' + str(paciente) + ' ' + fechastr + '.pdf' #Un string con el nombre del documento que tendra, con el usuario y la fecha de generacion
            global pacienteS
            pacienteS = str(paciente) #Un string solo con el dato de usuario del paciente
            print(id) #Dato de id del terapeuta para identificar correctamente el paciente

            #Se ponen globales las varibles para añadir datos y posteriormente usarlas en la funcion de pdf de generar pdf
            global nombreDatos
            global nombreCompleto
            global documentoPaciente
            global userTerapeuta
            global numeroEjer
            global nombresEjercicios
            global nombresErrores
            global numeroErrores
            global configEjercicios

            #Diccionarios y vectores para almacenar datos para visualizacion en consola (Ejercicios, errores, fallos)
            dicci = dict()
            docci = []
            vi = dict()
            ki = dict()
            ji = dict()

            #Extraccion y asignacion de informacion de "db" la base de datos previamente asignada 

            #Extrae toda la informacion del paciente escrito en el campo de texto de la interfaz 
            #(Get llama la info como diccionario asi que se asigna el key o el value )
            users = db.child("users").child(id).child("Usuarios").child(paciente).get() 
            
            #Extrae la informacion de nombre completo del usuario/paciente
            nombreCP = db.child("users").child(id).child("Usuarios").child(paciente).child('Nombre Completo').get()
            nombreCompleto = nombreCP.val() #Asigna el nombre completo a la variable global

            #Extrae la informacion de documento del usuario/paciente
            documentoCP = db.child("users").child(id).child("Usuarios").child(paciente).child('Documento').get()
            documentoPaciente = documentoCP.val() #Asigna el documento a la variable global 

            #Extrae toda la informacion debajo de la jerarquia del terapeuta
            userT = db.child("users").child(id).child("username").get()
            userTerapeuta = userT.val() #Asigna la informacion del terapeuta a la variable global

            #Diccionarios para la extraccion de informacion especifica del paciente
            dicci = users.val()  
            docci = users.val()
            ki = dicci['Ejercicio'] #Diccionario que almacena la informacion de los ejercicios
            ji = dicci['Errores'] #Diccionario que almacena la informacion de los intentos/errores (Dado el caso de haber perdido)

            #los 3 loops siguientes permiten añadir los datos a los diccionarios ya que la extraccion en la base
            #es de tipo diccioario asi que hay que asignar ya sean los keys o los values
            for clave in ki.keys():
                nombresEjercicios.append(clave)
                
            for clave in ji.keys():
                nombresErrores.append(clave)
                
            for clave in ki.values():
                configEjercicios.append(clave)
                
                
            #Extrae el numero de ejercicios y de ejercicios realizados (errores)
            #con base en la asignacion que se hizo en el loop
            numeroEjer = len(nombresEjercicios)
            numeroErrores = len(nombresErrores)

            pacienteDatos = [] #Vector de datos de paciente
            pacienteDi = dict() #Diccionario de datos de paciente
            
            #Dependiendo el numero de ejercicios, almacena en el vector la informacion de los ejercicios con fines visuales en consola
            for i in range(numeroEjer):
                datos = db.child("users").child(id).child("Usuarios").child(paciente).child('Ejercicio').child(nombresEjercicios[i]).child('Estante').get()
                pacienteDatos.append(datos.val())

            #Añade los elementos y la posicion de los objetos en el estante en cada loop hace una cosa
            for elem in pacienteDatos:      #accedemos a cada elemento de la lista (en este caso cada elemento es un dictionario)
                for k,v in elem.items():        #acedemos a cada llave(k), valor(v) de cada diccionario
                    print(k, v)

            #Añade el nombre de los espacios dele stante
            for elem in pacienteDatos:
                for k in elem.keys():
                    if k in nombreDatos:
                        pass
                    else:   
                        nombreDatos.append(k)
            #Muestra los datos en consola con fines meramente visuales
            print(nombreDatos)
            print(len(nombreDatos))

            #Muestra los ejercicios con fines visuales en consola (Ejercicios que tengan como nombre "Ejercicio1,2...")
            num1 = len(ki)
            for i in range(num1):
                ris = 'Ejercicio' + str(i+1)
                ejercicios.append(ris)

            #El siguiente while permite obtener todos los intentos que tenga el ejercicio en la zona de errores de la 
            #jerarquia de la base de datos para ver en la consola y agregar a las variables globales
            indice = 0
            while indice < num1:

                try:   

                    vi = dicci['Errores'][nombresEjercicios[indice]]
                    val = len(vi)
                    global num2
                    num2.append(val)
                    

                                
                    for i in range(val):
                        res = 'Intento '+ str(i)
                        intentos.append(res)

                    for i in range(val):
                        global fallas
                        fallas.append(dicci['Errores'][nombresEjercicios[indice]][intentos[i]]['Fallas'])
                        global tiempoSobrante
                        tiempoSobrante.append(dicci['Errores'][nombresEjercicios[indice]][intentos[i]]['Tiempo utilizado'])

                except KeyError:
                    print('Ese no existe')       
                indice += 1
            
            print(intentos)
            print(fallas)

            global tiempoSobranteE
            for i in range(len(tiempoSobrante)):
                tiempoSobranteE.append(int(tiempoSobrante[i]))

            print(tiempoSobranteE)
        
        #Si el paciente no tiene datos mostrara un aviso   
        except:
            self.datoconfi.setVisible(False)
            self.datofall.setVisible(True)
            self.BtnGenerar.setVisible(False)
        
    #Toda la funcion de generar pdf consta de la ubicacion de la informacion en las hojas del documento
    def generarPDF(self):
        
        #Se establecen unas varaibles para definir unos tamaños (int) de letra 
        SMALL_SIZE = 8 
        MEDIUM_SIZE = 10
        BIGGER_SIZE = 12

        #se establecen los tamaños de plt (importar)
        plt.rc('font', size=SMALL_SIZE)          # controls default text sizes
        plt.rc('axes', titlesize=SMALL_SIZE)     # fontsize of the axes title
        plt.rc('axes', labelsize=MEDIUM_SIZE)    # fontsize of the x and y labels
        plt.rc('xtick', labelsize=SMALL_SIZE)    # fontsize of the tick labels
        plt.rc('ytick', labelsize=SMALL_SIZE)    # fontsize of the tick labels
        plt.rc('legend', fontsize=SMALL_SIZE)    # legend fontsize
        plt.rc('figure', titlesize=BIGGER_SIZE)  # fontsize of the figure title
        

        #Se establece el tamaño de las hojas a tipo A4
        elcanvas = canvas.Canvas(pacienteN, pagesize=A4,) 

        #Toda la primera pagina hasta ver un comentario de tipo tal que ''' ----------------- ''' que separa las paginas
        #La pagina de introduccion muestra la informacion princiapl llamando la informacion de la funcion mostrar datos
        #La estructura es usando drawstring para ubicar el texto en la hoja del pdf  
        #y antes de eso si se quiere cambiar el tamaño de la letra y tipo
        elcanvas.setFont('Helvetica', 25)
        elcanvas.drawString(70 * mm, 280 * mm , "Reporte de " + pacienteS) #Nombre del reporte junto el del usuario
        elcanvas.setFont('Helvetica', 10)
        elcanvas.drawString(10 * mm, 270 * mm , "Nombre completo: " + nombreCompleto) #Nombre completo
        elcanvas.setFont('Helvetica', 10)
        elcanvas.drawString(10 * mm, 265 * mm , "Documento: " + documentoPaciente) #Documento
        elcanvas.drawString(10 * mm, 260 * mm , "Username terapeuta asignado: " + userTerapeuta) #username del terapeuta
        elcanvas.drawString(10 * mm, 250 * mm , "La cantidad de ejercicios configurados fueron: " + str(numeroEjer)) #cantidad de ejercicios
        
        #Se ubican los nombres de ejercicios siendo el maximo de 7 y la ubicacion de estos uno al lado del otro 
        if(numeroEjer == 1):
            elcanvas.drawString(10 * mm, 245 * mm , "Los nombres de estos ejercicios son: " + str(nombresEjercicios[0]))
        elif(numeroEjer == 2):
            elcanvas.drawString(10 * mm, 245 * mm , "Los nombres de estos ejercicios son: " + str(nombresEjercicios[0]) + ', ' + str(nombresEjercicios[1]))
        elif(numeroEjer == 3):
            elcanvas.drawString(10 * mm, 245 * mm , "Los nombres de estos ejercicios son: " + str(nombresEjercicios[0]) + ', ' +  str(nombresEjercicios[1]) + ', ' +  str(nombresEjercicios[2]))
        elif(numeroEjer == 4):
            elcanvas.drawString(10 * mm, 245 * mm , "Los nombres de estos ejercicios son: " + str(nombresEjercicios[0]) + ', ' +  str(nombresEjercicios[1]) + ', ' +  str(nombresEjercicios[2]) + ', ' +  str(nombresEjercicios[3]))
        elif(numeroEjer == 5):
            elcanvas.drawString(10 * mm, 245 * mm , "Los nombres de estos ejercicios son: " + str(nombresEjercicios[0]) + ', ' +  str(nombresEjercicios[1]) + ', ' +  str(nombresEjercicios[2]) + ', ' +  str(nombresEjercicios[3]) + ', ' +  str(nombresEjercicios[4]))
        elif(numeroEjer == 6):
            elcanvas.drawString(10 * mm, 245 * mm , "Los nombres de estos ejercicios son: " + str(nombresEjercicios[0]) + ', ' +  str(nombresEjercicios[1]) + ', ' +  str(nombresEjercicios[2]) + ', ' +  str(nombresEjercicios[3]) + ', ' +  str(nombresEjercicios[4]) + ', ' +  str(nombresEjercicios[5]))
        elif(numeroEjer == 7):
            elcanvas.drawString(10 * mm, 245 * mm , "Los nombres de estos ejercicios son: " + str(nombresEjercicios[0]) + ', ' +  str(nombresEjercicios[1]) + ', ' +  str(nombresEjercicios[2]) + ', ' +  str(nombresEjercicios[3]) + ', ' +  str(nombresEjercicios[4]) + ', ' +  str(nombresEjercicios[5]) + ', ' +  str(nombresEjercicios[6]))
        
        #Titulo Estante
        elcanvas.setFont('Helvetica', 20)
        elcanvas.drawString(40 * mm, 230 * mm , "Estante")

        #Titulo Objetos
        elcanvas.setFont('Helvetica', 20)
        elcanvas.drawString(140 * mm, 230 * mm , "Objetos")

        elcanvas.setFont('Helvetica', 10)

        #Ubicacion de los estantes (se ponen los 4 lados del estante en imagenes de la carpeta "Imagenes")
        elcanvas.drawString(25 * mm, 225 * mm , "Estante frontal (EF) posiciones:")
        imagen1 = "Imagenes/FrontalNuevoRender.PNG"
        elcanvas.drawImage(imagen1, 20*mm, 170*mm , width=  65*mm ,  height= 65*mm,  preserveAspectRatio=True)

        elcanvas.drawString(25 * mm, 175 * mm , "Estante posterior (EP) posiciones:")
        imagen2 = "Imagenes/PosteriorNuevoRen.png"
        elcanvas.drawImage(imagen2, 20*mm, 120*mm , width=  65*mm ,  height= 65*mm,  preserveAspectRatio=True)

        elcanvas.drawString(25 * mm, 125 * mm , "Estante lateral rojo (ELR) posiciones:")
        imagen3 = "Imagenes/LateralRojoRen.png"
        elcanvas.drawImage(imagen3, 27*mm, 70*mm , width=  50*mm ,  height= 50*mm,  preserveAspectRatio=True)

        elcanvas.drawString(25 * mm, 65 * mm , "Estante lateral amarillo (ELA) posiciones:")
        imagen4 = "Imagenes/LateralAmarilloRen.png"
        elcanvas.drawImage(imagen4, 27*mm, 10*mm , width=  50*mm ,  height= 50*mm,  preserveAspectRatio=True)

        #Se ponen los objetos (Se ponen los 30 objetos de la carpeta Imagenes)
        objeto1 = "Imagenes/BananaR.png"
        objeto2 = "Imagenes/BananaVariante.png"
        objeto3 = "Imagenes/CerealEstrella.png"
        objeto4 = "Imagenes/CerealCorazon.png"
        objeto5 = "Imagenes/CremaManosRemRoja.png"
        objeto6 = "Imagenes/CremaManosVariAma.png"
        objeto7 = "Imagenes/JugoNaranjaRem.png"
        objeto8 = "Imagenes/JugoNaranjaVari.png"
        objeto9 = "Imagenes/LecheRempl.png"
        objeto10 = "Imagenes/LecheVari.png"
        objeto11 = "Imagenes/ManzanaR.png"
        objeto12 = "Imagenes/ManzanaVerde.png"
        objeto13 = "Imagenes/NaranjaR.png"
        objeto14 = "Imagenes/NaranjaVari.png"
        objeto15 = "Imagenes/PeraR.png"
        objeto16 = "Imagenes/PeraVari.png"
        objeto17 = "Imagenes/Poccky.png"
        objeto18 = "Imagenes/PockyVari.png"
        objeto19 = "Imagenes/RosquillasRem.png"
        objeto20 = "Imagenes/RosquillasVari.png"
        objeto21 = "Imagenes/SalsaR.png"
        objeto22 = "Imagenes/SalsaTomanteVari.png"
        objeto23 = "Imagenes/SalsaRem.png"
        objeto24 = "Imagenes/SalsaVari.png"
        objeto25 = "Imagenes/SodaAzul.png"
        objeto26 = "Imagenes/SodaRoja.png"
        objeto27 = "Imagenes/UvasR.png"
        objeto28 = "Imagenes/UvasVari.png"
        objeto29 = "Imagenes/Water.png"
        objeto30 = "Imagenes/AguaVariante.png"

        #Se ponen los nombres encima de cada imagen de objeto
        elcanvas.setFont('Helvetica', 7)
        elcanvas.drawString(110 * mm, 225 * mm , "Banana")
        elcanvas.drawImage(objeto1, 105*mm, 200*mm , width=  20*mm ,  height= 20*mm,  preserveAspectRatio=True)
        elcanvas.drawString(130 * mm, 225 * mm , "BananaVari")
        elcanvas.drawImage(objeto2, 125*mm, 200*mm , width=  20*mm ,  height= 20*mm,  preserveAspectRatio=True)
        elcanvas.drawString(150 * mm, 225 * mm , "Cereal")
        elcanvas.drawImage(objeto3, 145*mm, 200*mm , width=  20*mm ,  height= 20*mm,  preserveAspectRatio=True)
        elcanvas.drawString(170 * mm, 225 * mm , "CerealVari")
        elcanvas.drawImage(objeto4, 165*mm, 200*mm , width=  20*mm ,  height= 20*mm,  preserveAspectRatio=True)
        elcanvas.drawString(110 * mm, 195 * mm , "Crema")
        elcanvas.drawImage(objeto5, 105*mm, 170*mm , width=  20*mm ,  height= 20*mm,  preserveAspectRatio=True)
        elcanvas.drawString(130 * mm, 195 * mm , "CremaVari")
        elcanvas.drawImage(objeto6, 125*mm, 170*mm , width=  20*mm ,  height= 20*mm,  preserveAspectRatio=True)
        elcanvas.drawString(150 * mm, 195 * mm , "JuNaranja")
        elcanvas.drawImage(objeto7, 145*mm, 170*mm , width=  20*mm ,  height= 20*mm,  preserveAspectRatio=True)
        elcanvas.drawString(170 * mm, 195 * mm , "JuNaranjaVari")
        elcanvas.drawImage(objeto8, 165*mm, 170*mm , width=  20*mm ,  height= 20*mm,  preserveAspectRatio=True)
        elcanvas.drawString(110 * mm, 165 * mm , "Leche")
        elcanvas.drawImage(objeto9, 105*mm, 140*mm , width=  20*mm ,  height= 20*mm,  preserveAspectRatio=True)
        elcanvas.drawString(130 * mm, 165 * mm , "LecheVari")
        elcanvas.drawImage(objeto10, 125*mm, 140*mm , width=  20*mm ,  height= 20*mm,  preserveAspectRatio=True)
        elcanvas.drawString(150 * mm, 165 * mm , "Manzana")
        elcanvas.drawImage(objeto11, 145*mm, 140*mm , width=  20*mm ,  height= 20*mm,  preserveAspectRatio=True)
        elcanvas.drawString(170 * mm, 165 * mm , "ManzanaVari")
        elcanvas.drawImage(objeto12, 165*mm, 140*mm , width=  20*mm ,  height= 20*mm,  preserveAspectRatio=True)
        elcanvas.drawString(110 * mm, 135 * mm , "Naranja")
        elcanvas.drawImage(objeto13, 105*mm, 110*mm , width=  20*mm ,  height= 20*mm,  preserveAspectRatio=True)
        elcanvas.drawString(130 * mm, 135 * mm , "NaranjaVari")
        elcanvas.drawImage(objeto14, 125*mm, 110*mm , width=  20*mm ,  height= 20*mm,  preserveAspectRatio=True)
        elcanvas.drawString(150 * mm, 135 * mm , "Pera")
        elcanvas.drawImage(objeto15, 145*mm, 110*mm , width=  20*mm ,  height= 20*mm,  preserveAspectRatio=True)
        elcanvas.drawString(170 * mm, 135 * mm , "PeraVari")
        elcanvas.drawImage(objeto16, 165*mm, 110*mm , width=  20*mm ,  height= 20*mm,  preserveAspectRatio=True)
        elcanvas.drawString(110 * mm, 105 * mm , "Pocky")
        elcanvas.drawImage(objeto17, 105*mm, 80*mm , width=  20*mm ,  height= 20*mm,  preserveAspectRatio=True)
        elcanvas.drawString(130 * mm, 105 * mm , "PockyVari")
        elcanvas.drawImage(objeto18, 125*mm, 80*mm , width=  20*mm ,  height= 20*mm,  preserveAspectRatio=True)
        elcanvas.drawString(150 * mm, 105 * mm , "Rosquillas")
        elcanvas.drawImage(objeto19, 145*mm, 80*mm , width=  20*mm ,  height= 20*mm,  preserveAspectRatio=True)
        elcanvas.drawString(170 * mm, 105 * mm , "RosquillasVari")
        elcanvas.drawImage(objeto20, 165*mm, 80*mm , width=  20*mm ,  height= 20*mm,  preserveAspectRatio=True)
        elcanvas.drawString(110 * mm, 75 * mm , "SalsaTomate")
        elcanvas.drawImage(objeto21, 105*mm, 50*mm , width=  20*mm ,  height= 20*mm,  preserveAspectRatio=True)
        elcanvas.drawString(130 * mm, 75 * mm , "SalsaTomateVari")
        elcanvas.drawImage(objeto22, 125*mm, 50*mm , width=  20*mm ,  height= 20*mm,  preserveAspectRatio=True)
        elcanvas.drawString(150 * mm, 75 * mm , "SalsaRosada")
        elcanvas.drawImage(objeto23, 145*mm, 50*mm , width=  20*mm ,  height= 20*mm,  preserveAspectRatio=True)
        elcanvas.drawString(170 * mm, 75 * mm , "SalsaRosadaVari")
        elcanvas.drawImage(objeto24, 165*mm, 50*mm , width=  20*mm ,  height= 20*mm,  preserveAspectRatio=True)
        elcanvas.drawString(110 * mm, 48 * mm , "Soda")
        elcanvas.drawImage(objeto25, 105*mm, 27*mm , width=  20*mm ,  height= 20*mm,  preserveAspectRatio=True)
        elcanvas.drawString(130 * mm, 48 * mm , "SodaVari")
        elcanvas.drawImage(objeto26, 125*mm, 27*mm , width=  20*mm ,  height= 20*mm,  preserveAspectRatio=True)
        elcanvas.drawString(150 * mm, 48 * mm , "Uvas")
        elcanvas.drawImage(objeto27, 145*mm, 27*mm , width=  20*mm ,  height= 20*mm,  preserveAspectRatio=True)
        elcanvas.drawString(170 * mm, 48 * mm , "UvasVari")
        elcanvas.drawImage(objeto28, 165*mm, 27*mm , width=  20*mm ,  height= 20*mm,  preserveAspectRatio=True)
        elcanvas.drawString(110 * mm, 22 * mm , "Agua")
        elcanvas.drawImage(objeto29, 105*mm, 0*mm , width=  20*mm ,  height= 20*mm,  preserveAspectRatio=True)
        elcanvas.drawString(130 * mm, 22 * mm , "AguaVari")
        elcanvas.drawImage(objeto30, 125*mm, 0*mm , width=  20*mm ,  height= 20*mm,  preserveAspectRatio=True)
        elcanvas.showPage()

        ''' ------------------------------------------------------------------------------ '''

        #Sigue la pagina de descripcion del ejercicio    
        for j in range(numeroErrores):
            
            #Titulo del reporte
            elcanvas.setFont('Helvetica', 25)
            elcanvas.drawString(70 * mm, 280 * mm , "Reporte de " + pacienteS)

            #Nombre del ejercicio efectuado
            elcanvas.setFont('Helvetica', 15)
            elcanvas.drawString(70 * mm, 270 * mm , str(nombresErrores[j]) + " configuracion.")   

            #Identificar, extraer y poner el tiempo configurado del ejercicio
            elcanvas.setFont('Helvetica', 10)
            Tiempo = db.child("users").child(id).child("Usuarios").child(paciente).child('Ejercicio').child(nombresErrores[j]).child('Estante').child(nombreDatos[1]).get()
            Tempo = Tiempo.val()
            elcanvas.drawString(10 * mm, 260 * mm , "El tiempo configurado fue de: " + str(Tempo))
            global tiempoglo
            tiempoglo = int(Tempo)
            
            #Identificar, extraer y poner la ubicacion del monster 
            Monster = db.child("users").child(id).child("Usuarios").child(paciente).child('Ejercicio').child(nombresErrores[j]).child('Estante').child(nombreDatos[0]).get()
            MON = Monster.val()
            elcanvas.drawString(10 * mm, 250 * mm , "La ubicacion del monster fue la numero: " + str(MON))

            #Espacios en los estantes y su respectivo objeto asignado
            Estante1 = db.child("users").child(id).child("Usuarios").child(paciente).child('Ejercicio').child(nombresErrores[j]).child('Estante').child(nombreDatos[2]).get()
            EF1 = Estante1.val()
            elcanvas.drawString(10 * mm, 230 * mm , "EF1 tiene un(a): " + str(EF1))
            
            Estante2 = db.child("users").child(id).child("Usuarios").child(paciente).child('Ejercicio').child(nombresErrores[j]).child('Estante').child(nombreDatos[6]).get()
            EF2 = Estante2.val()
            elcanvas.drawString(10 * mm, 220 * mm , "EF2 tiene un(a): " + str(EF2))
            
            Estante3 = db.child("users").child(id).child("Usuarios").child(paciente).child('Ejercicio').child(nombresErrores[j]).child('Estante').child(nombreDatos[7]).get()
            EF3 = Estante3.val()
            elcanvas.drawString(10 * mm, 210 * mm , "EF3 tiene un(a): " + str(EF3))
            
            Estante4 = db.child("users").child(id).child("Usuarios").child(paciente).child('Ejercicio').child(nombresErrores[j]).child('Estante').child(nombreDatos[8]).get()
            EF4 = Estante4.val()
            elcanvas.drawString(10 * mm, 200 * mm , "EF4 tiene un(a): " + str(EF4))
            
            Estante5 = db.child("users").child(id).child("Usuarios").child(paciente).child('Ejercicio').child(nombresErrores[j]).child('Estante').child(nombreDatos[9]).get()
            EF5 = Estante5.val()
            elcanvas.drawString(10 * mm, 190 * mm , "EF5 tiene un(a): " + str(EF5))
            
            Estante6 = db.child("users").child(id).child("Usuarios").child(paciente).child('Ejercicio').child(nombresErrores[j]).child('Estante').child(nombreDatos[10]).get()
            EF6 = Estante6.val()
            elcanvas.drawString(10 * mm, 180 * mm , "EF6 tiene un(a): " + str(EF6))
            
            Estante7 = db.child("users").child(id).child("Usuarios").child(paciente).child('Ejercicio').child(nombresErrores[j]).child('Estante').child(nombreDatos[11]).get()
            EF7 = Estante7.val()
            elcanvas.drawString(10 * mm, 170 * mm , "EF7 tiene un(a): " + str(EF7))
            
            Estante8 = db.child("users").child(id).child("Usuarios").child(paciente).child('Ejercicio').child(nombresErrores[j]).child('Estante').child(nombreDatos[12]).get()
            EF8 = Estante8.val()
            elcanvas.drawString(10 * mm, 160 * mm , "EF8 tiene un(a): " + str(EF8))
            
            Estante9 = db.child("users").child(id).child("Usuarios").child(paciente).child('Ejercicio').child(nombresErrores[j]).child('Estante').child(nombreDatos[13]).get()
            EF9 = Estante9.val()
            elcanvas.drawString(10 * mm, 150 * mm , "EF9 tiene un(a): " + str(EF9))
            
            Estante10 = db.child("users").child(id).child("Usuarios").child(paciente).child('Ejercicio').child(nombresErrores[j]).child('Estante').child(nombreDatos[3]).get()
            EF10 = Estante10.val()
            elcanvas.drawString(10 * mm, 140 * mm , "EF10 tiene un(a): " + str(EF10))
            
            Estante11 = db.child("users").child(id).child("Usuarios").child(paciente).child('Ejercicio').child(nombresErrores[j]).child('Estante').child(nombreDatos[4]).get()
            EF11 = Estante11.val()
            elcanvas.drawString(10 * mm, 130 * mm , "EF11 tiene un(a): " + str(EF11))
            
            Estante12 = db.child("users").child(id).child("Usuarios").child(paciente).child('Ejercicio').child(nombresErrores[j]).child('Estante').child(nombreDatos[5]).get()
            EF12 = Estante12.val()
            elcanvas.drawString(10 * mm, 120 * mm , "EF12 tiene un(a): " + str(EF12))

            #Se extrae y se pone en la esquina inferior izquierda, el numero de retos y los retos (objetivos) asignados a ese ejercicio
            Re = dict()
            Retos = db.child("users").child(id).child("Usuarios").child(paciente).child('Ejercicio').child(nombresErrores[j]).child('Reto').get()
            Re = Retos.val()
            numRe = len(Re)
            NomRe = []
            CanRe = []
            ubicacion = 90
            global totalBuscar 
            totalBuscar = 0
            elcanvas.drawString(5 * mm, 100 * mm , "El numero de retos fue de: " + str(numRe+1) + " incluyendo el monster y fueron los siguientes objetos.")
            elcanvas.drawString(15 * mm, 95 * mm , "El objeto fue monster con una cantidad no modificable de 1")

            for clave in Re.keys():
                NomRe.append(clave)
            
            for clave in Re.values():
                CanRe.append(clave)
                totalBuscar = totalBuscar + int(clave)
            
            for b in range(numRe):
                elcanvas.drawString(15 * mm, ubicacion * mm , "El objeto fue " + str(NomRe[b]) + " con una cantidad de " +  str(CanRe[b]))
                ubicacion = ubicacion - 5

           
            #Se continua con los espacios de los estantes y su respectivo objeto asignado
            EstanteP1 = db.child("users").child(id).child("Usuarios").child(paciente).child('Ejercicio').child(nombresErrores[j]).child('Estante').child(nombreDatos[14]).get()
            EP1 = EstanteP1.val()
            elcanvas.drawString(80 * mm, 230 * mm , "EP1 tiene un(a): " + str(EP1))
            
            EstanteP2 = db.child("users").child(id).child("Usuarios").child(paciente).child('Ejercicio').child(nombresErrores[j]).child('Estante').child(nombreDatos[18]).get()
            EP2 = EstanteP2.val()
            elcanvas.drawString(80 * mm, 220 * mm , "EP2 tiene un(a): " + str(EP2))
            
            EstanteP3 = db.child("users").child(id).child("Usuarios").child(paciente).child('Ejercicio').child(nombresErrores[j]).child('Estante').child(nombreDatos[19]).get()
            EP3 = EstanteP3.val()
            elcanvas.drawString(80 * mm, 210 * mm , "EP3 tiene un(a): " + str(EP3))
            
            EstanteP4 = db.child("users").child(id).child("Usuarios").child(paciente).child('Ejercicio').child(nombresErrores[j]).child('Estante').child(nombreDatos[20]).get()
            EP4 = EstanteP4.val()
            elcanvas.drawString(80 * mm, 200 * mm , "EP4 tiene un(a): " + str(EP4))
            
            EstanteP5 = db.child("users").child(id).child("Usuarios").child(paciente).child('Ejercicio').child(nombresErrores[j]).child('Estante').child(nombreDatos[21]).get()
            EP5 = EstanteP5.val()
            elcanvas.drawString(80 * mm, 190 * mm , "EP5 tiene un(a): " + str(EP5))
            
            EstanteP6 = db.child("users").child(id).child("Usuarios").child(paciente).child('Ejercicio').child(nombresErrores[j]).child('Estante').child(nombreDatos[22]).get()
            EP6 = EstanteP6.val()
            elcanvas.drawString(80 * mm, 180 * mm , "EF6 tiene un(a): " + str(EP6))
            
            EstanteP7 = db.child("users").child(id).child("Usuarios").child(paciente).child('Ejercicio').child(nombresErrores[j]).child('Estante').child(nombreDatos[23]).get()
            EP7 = EstanteP7.val()
            elcanvas.drawString(80 * mm, 170 * mm , "EP7 tiene un(a): " + str(EP7))
            
            EstanteP8 = db.child("users").child(id).child("Usuarios").child(paciente).child('Ejercicio').child(nombresErrores[j]).child('Estante').child(nombreDatos[24]).get()
            EP8 = EstanteP8.val()
            elcanvas.drawString(80 * mm, 160 * mm , "EP8 tiene un(a): " + str(EP8))
            
            EstanteP9 = db.child("users").child(id).child("Usuarios").child(paciente).child('Ejercicio').child(nombresErrores[j]).child('Estante').child(nombreDatos[25]).get()
            EP9 = EstanteP9.val()
            elcanvas.drawString(80 * mm, 150 * mm , "EP9 tiene un(a): " + str(EP9))
            
            EstanteP10 = db.child("users").child(id).child("Usuarios").child(paciente).child('Ejercicio').child(nombresErrores[j]).child('Estante').child(nombreDatos[15]).get()
            EP10 = EstanteP10.val()
            elcanvas.drawString(80 * mm, 140 * mm , "EP10 tiene un(a): " + str(EP10))
            
            EstanteP11 = db.child("users").child(id).child("Usuarios").child(paciente).child('Ejercicio').child(nombresErrores[j]).child('Estante').child(nombreDatos[16]).get()
            EP11 = EstanteP11.val()
            elcanvas.drawString(80 * mm, 130 * mm , "EP11 tiene un(a): " + str(EP11))
            
            EstanteP12 = db.child("users").child(id).child("Usuarios").child(paciente).child('Ejercicio').child(nombresErrores[j]).child('Estante').child(nombreDatos[17]).get()
            EP12 = EstanteP12.val()
            elcanvas.drawString(80 * mm, 120 * mm , "EP12 tiene un(a): " + str(EP12))


            EstanteLA1 = db.child("users").child(id).child("Usuarios").child(paciente).child('Ejercicio').child(nombresErrores[j]).child('Estante').child(nombreDatos[26]).get()
            ELA1 = EstanteLA1.val()
            elcanvas.drawString(140 * mm, 230 * mm , "ELA1 tiene un(a): " + str(ELA1))
            
            EstanteLA2 = db.child("users").child(id).child("Usuarios").child(paciente).child('Ejercicio').child(nombresErrores[j]).child('Estante').child(nombreDatos[27]).get()
            ELA2 = EstanteLA2.val()
            elcanvas.drawString(140 * mm, 220 * mm , "ELA2 tiene un(a): " + str(ELA2))
            
            EstanteLA3 = db.child("users").child(id).child("Usuarios").child(paciente).child('Ejercicio').child(nombresErrores[j]).child('Estante').child(nombreDatos[28]).get()
            ELA3 = EstanteLA3.val()
            elcanvas.drawString(140 * mm, 210 * mm , "ELA3 tiene un(a): " + str(ELA3))
            
            EstanteLA4 = db.child("users").child(id).child("Usuarios").child(paciente).child('Ejercicio').child(nombresErrores[j]).child('Estante').child(nombreDatos[29]).get()
            ELA4 = EstanteLA4.val()
            elcanvas.drawString(140 * mm, 200 * mm , "ELA4 tiene un(a): " + str(ELA4))
            
            EstanteLA5 = db.child("users").child(id).child("Usuarios").child(paciente).child('Ejercicio').child(nombresErrores[j]).child('Estante').child(nombreDatos[30]).get()
            ELA5 = EstanteLA5.val()
            elcanvas.drawString(140 * mm, 190 * mm , "ELA5 tiene un(a): " + str(ELA5))
            
            EstanteLA6 = db.child("users").child(id).child("Usuarios").child(paciente).child('Ejercicio').child(nombresErrores[j]).child('Estante').child(nombreDatos[31]).get()
            ELA6 = EstanteLA6.val()
            elcanvas.drawString(140 * mm, 180 * mm , "ELA6 tiene un(a): " + str(ELA6))
            
            EstanteLA7 = db.child("users").child(id).child("Usuarios").child(paciente).child('Ejercicio').child(nombresErrores[j]).child('Estante').child(nombreDatos[32]).get()
            ELA7 = EstanteLA7.val()
            elcanvas.drawString(140 * mm, 170 * mm , "ELA7 tiene un(a): " + str(ELA7))
            
            EstanteLA8 = db.child("users").child(id).child("Usuarios").child(paciente).child('Ejercicio').child(nombresErrores[j]).child('Estante').child(nombreDatos[33]).get()
            ELA8 = EstanteLA8.val()
            elcanvas.drawString(140 * mm, 160 * mm , "ELA8 tiene un(a): " + str(ELA8))

            #Se continua con la carga del objeto en el espacio del estante y su colocacion en el pdf

            EstanteLR1 = db.child("users").child(id).child("Usuarios").child(paciente).child('Ejercicio').child(nombresErrores[j]).child('Estante').child(nombreDatos[34]).get()
            ELR1 = EstanteLR1.val()
            elcanvas.drawString(140 * mm, 140 * mm , "ELR1 tiene un(a): " + str(ELA1))
            
            EstanteLR2 = db.child("users").child(id).child("Usuarios").child(paciente).child('Ejercicio').child(nombresErrores[j]).child('Estante').child(nombreDatos[35]).get()
            ELR2 = EstanteLR2.val()
            elcanvas.drawString(140 * mm, 130 * mm , "ELR2 tiene un(a): " + str(ELA2))
            
            EstanteLR3 = db.child("users").child(id).child("Usuarios").child(paciente).child('Ejercicio').child(nombresErrores[j]).child('Estante').child(nombreDatos[36]).get()
            ELR3 = EstanteLR3.val()
            elcanvas.drawString(140 * mm, 120 * mm , "ELR3 tiene un(a): " + str(ELA3))
            
            EstanteLR4 = db.child("users").child(id).child("Usuarios").child(paciente).child('Ejercicio').child(nombresErrores[j]).child('Estante').child(nombreDatos[37]).get()
            ELR4 = EstanteLR4.val()
            elcanvas.drawString(140 * mm, 110 * mm , "ELR4 tiene un(a): " + str(ELA4))
            
            EstanteLR5 = db.child("users").child(id).child("Usuarios").child(paciente).child('Ejercicio').child(nombresErrores[j]).child('Estante').child(nombreDatos[38]).get()
            ELR5 = EstanteLR5.val()
            elcanvas.drawString(140 * mm, 100 * mm , "ELR5 tiene un(a): " + str(ELA5))
            
            EstanteLR6 = db.child("users").child(id).child("Usuarios").child(paciente).child('Ejercicio').child(nombresErrores[j]).child('Estante').child(nombreDatos[39]).get()
            ELR6 = EstanteLR6.val()
            elcanvas.drawString(140 * mm, 90 * mm , "ELR6 tiene un(a): " + str(ELA6))

            elcanvas.showPage() #Cada vez que aparezca este comando es un cambio de pagina 

            ''' ------------------------------------------------------------------------------ '''
            #Pagina de resultados
            #Informacion titular
            elcanvas.setFont('Helvetica', 25)
            elcanvas.drawString(70 * mm, 280 * mm , "Reporte de " + pacienteS)
            elcanvas.setFont('Helvetica', 15)
            elcanvas.drawString(70 * mm, 270 * mm , str(nombresErrores[j]) + " resultados")

            #Fecha de inicio y de fin en la informacion titular
            elcanvas.setFont('Helvetica', 10)
            fechaIni = db.child("users").child(id).child("Usuarios").child(paciente).child('Ejercicio').child(nombresErrores[j]).child('FechaInicio').get()
            fechaFin = db.child("users").child(id).child("Usuarios").child(paciente).child('Ejercicio').child(nombresErrores[j]).child('FechaFin').get()
            elcanvas.drawString(10 * mm, 260 * mm , "Este ejercicio tuvo como fecha de inicio: " + str(fechaIni.val()) + " y su fecha de finalizacion fue: " + str(fechaFin.val()))
            
            #Se extrae la informacion de los intentos y los errores dentro
            canErr = db.child("users").child(id).child("Usuarios").child(paciente).child('Errores').child(nombresErrores[j]).get()
            x = dict()
            x = canErr.val()
            si = x.keys()
            fi = si.__len__()

            #El numero de intentos para ubicar en la hoja
            elcanvas.drawString(10 * mm, 250 * mm , "Realizo un numero de intentos de: " + str(fi))
            #Dependiendo el numero de intentos se agregan a un vector con cada uno de ellos para extraerlos luego en 
            #la base de datos (bd) los llama por esos nombres
            listaIntentos = []
            for i in range(fi):
                listaIntentos.append('Intento ' + str(i))

            #Variables que se usan en esta pagina para ubicacion del texto y conteos, tambien para almacenar los datos
            floti = 240
            indice = 0
            aciertos = {}
            fallas = {}
            tiempoutil = {}
            a = j
            #Vectores que almacenan los aciertos, los fallos y el tiempo utilizado
            infoIntentoA = []
            infoIntentoF = []
            infoIntentoT = []
            
            #El loop permite extrar la informacion de cada intento (aciertos, fallos y tiempo utilizado) de la base de datos 
            #y ponerla en los vectores para convertirlas en numeros (int) y hacer operaciones graficas y matematicas
            for dato in listaIntentos:
                aciertos = db.child("users").child(id).child("Usuarios").child(paciente).child('Errores').child(nombresErrores[a]).child(dato).child('Aciertos').get()
                fallas = db.child("users").child(id).child("Usuarios").child(paciente).child('Errores').child(nombresErrores[a]).child(dato).child('Fallas').get()
                tiempoutil = db.child("users").child(id).child("Usuarios").child(paciente).child('Errores').child(nombresErrores[a]).child(dato).child('Tiempo utilizado').get()
                elcanvas.drawString(10 * mm, floti * mm , "En el " + dato + ": tuvo un total de " + str(aciertos.val()) + " aciertos, " + str(fallas.val()) + " fallas y un tiempo utilizado de " + str(tiempoutil.val()))
                
                infoIntentoA.append(aciertos.val())
                infoIntentoF.append(fallas.val())
                infoIntentoT.append(tiempoutil.val())

                floti = floti - 5
            
            l1 = len(infoIntentoA)
            l2 = len(infoIntentoF)
            l3 = len(infoIntentoT)

            infoIntentoAN = []
            infoIntentoFN = []
            infoIntentoTN = []

            #Se hace la convercion a integer
            for h1 in range(l1):
                infoIntentoAN.append(int(infoIntentoA[h1]))
            for h2 in range(l2):
                infoIntentoFN.append(int(infoIntentoF[h2]))
            for h3 in range(l3):
                infoIntentoTN.append(int(infoIntentoT[h3]))
            
            print(infoIntentoAN)
            print(infoIntentoFN)
            print(infoIntentoTN)

            infoIntentoANsobreTotal = []
            infoIntentoFNsobreTotal = []
            infoIntentoTNsobreTotal = []
            #Se establece un vector con los porcentajes que se van a ver en las graficas
            porcentajes = [0, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100]

            #Se saca el factor de acierto que consta de el numero de aciertos sobre el numero de objetos a buscar 
            #por 100, para el porcentaje
            for h4 in range(len(infoIntentoAN)):
                Ar = (infoIntentoAN[h4]/(totalBuscar+1))*100
                if Ar < 100: 
                    infoIntentoANsobreTotal.append(int(Ar))
                else:
                    infoIntentoANsobreTotal.append(100)

            #Crea el tiempo utilizado reduciendo del sobrante pero esta funcion no se usa ya que en la base de datos
            #se sube el tiempo utilizado
            for h6 in range(len(infoIntentoTN)):
                infoIntentoTNsobreTotal.append(int((tiempoglo - infoIntentoTN[h6])))


            #La primera grafica de intentos por factor de acierto, almacena la imagen en la carpeta y se sobrescribe cada vez
            fig,ax = plt.subplots() 
            ax.plot(listaIntentos, infoIntentoANsobreTotal) 
            plt.ylabel('Factor acierto (%)')
            plt.xlabel('Intentos')
            plt.grid(True)
            plt.savefig('graficoA.png')
            gfa = ImageReader('graficoA.png')
            elcanvas.drawImage(gfa,   10*mm,   70*mm, width=100*mm,   preserveAspectRatio=True)

            #La segunda grafica de intentos por fallos, almacena la imagen en la carpeta y se sobrescribe cada vez
            fig,fx = plt.subplots() 
            fx.plot(listaIntentos, infoIntentoFN) 
            plt.ylabel('Numero de fallos')
            plt.xlabel('Intentos')
            plt.grid(True)
            plt.savefig('graficoF.png')
            gff = ImageReader('graficoF.png')
            elcanvas.drawImage(gff,   110*mm,   70*mm, width=100*mm,   preserveAspectRatio=True)

            #La tercera grafica de intentos por tiempo utilizado 
            fig,tx = plt.subplots() 
            tx.plot(listaIntentos, infoIntentoTN) 
            plt.ylabel('Tiempo utilizado (segundos)')
            plt.xlabel('Intentos')
            plt.grid(True)
            plt.savefig('graficoT.png')
            gft = ImageReader('graficoT.png')
            elcanvas.drawImage(gft,   110*mm,   -20*mm, width=100*mm,   preserveAspectRatio=True)

            #Saca la media del tiempo utilizado contado todo los tiempos de todos los intentos y usando la funcion mean 
            mediaTU = np.mean(infoIntentoTN) #Media
            mediaTS = np.mean(infoIntentoTNsobreTotal)
            #Pone en la hoja el resultado en la ubicacion 
            elcanvas.drawString(10 * mm, 90 * mm , "La media del tiempo utilizado es: " + str(round(mediaTU,1)) + " segundos")

            #Saca la varianza del tiempo utlizado contando todo los tiempos de todos los intentos utilizando la funcion var
            varianzaTU = np.var(infoIntentoTN) #Varianza
            varianzaTS = np.var(infoIntentoTNsobreTotal)
            #Pone en la hoja el resultado en la ubicacion
            elcanvas.drawString(10 * mm, 70 * mm , "La varianza del tiempo utilizado es: " + str(round(varianzaTU,2)) + " segundos")
            elcanvas.showPage()

        elcanvas.save() #Guarda el pdf
        self.confirm.setVisible(True) #Confima en pantalla que el pdf fue generado

#Caracteristicas del modulo, que se proyecte en ventana, la primera pagina sea login y hace el show       
app=QApplication(sys.argv)
mainwindow=Login()
widget=QtWidgets.QStackedWidget()
widget.addWidget(mainwindow)
widget.setFixedWidth(480)
widget.setFixedHeight(620)
widget.show()
app.exec_()
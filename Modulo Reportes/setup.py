#Este script permite la generacion del ejecutable del modulo usando cx_Freeze

import sys
import os

from cx_Freeze import setup, Executable

files = ['createacc.ui', 'Imagenes','login.ui'] #Llama las interfaces y la carpeta con las imagenes para el pdf

exe = Executable(script="main.py", base="Win32GUI", icon ="LogoReportes.ico") #Genera el ejecutable con base al script main.py y le establece un icono

#Añade datos de ejecucion, version, nombres y demas
setup(
    name = "MiniSuper AR Reportes",
    version = "1.0",
    description = "Este modulo se encarga de generar el reporte PDF del paciente que se elija",
    author = "Rosmer Yepes",
    options = {'build_exe':{'include_files': files}},
    executables =[exe]
)

#Se usa el codigo en consola para ejecutar este script:
#python setup.py build
#Creara una carpeta build, con el ejecutable y los archivos necesarios que previamente se establecieron en files y exe
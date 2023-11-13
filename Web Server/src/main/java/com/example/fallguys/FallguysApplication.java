package com.example.fallguys;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.annotation.PropertySource;

@SpringBootApplication
@PropertySource(value = { "classpath:application-auth.properties" })
public class FallguysApplication {

	public static void main(String[] args) {
		SpringApplication.run(FallguysApplication.class, args);
	}

}
